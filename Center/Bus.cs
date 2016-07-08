using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Bus
    {
        internal static Dictionary<Assembly, Constructure> Components = new Dictionary<Assembly, Constructure>();
        static Dictionary<Port, PortCollection> mOutIns = new Dictionary<Port, PortCollection>();
        static Dictionary<Port, WatcherValue> mListeners = new Dictionary<Port, WatcherValue>();

        internal static void OnLoadAssambly(Assembly asm)
        {
            if (Components.Keys.Contains(asm))
                return;

            Constructure structure = new Constructure();
            Components.Add(asm, structure);

            Dictionary<int, Port> inputPorts = new Dictionary<int, Port>();
            Dictionary<int, Port> outputPorts = new Dictionary<int, Port>();

            foreach (var def in asm.DefinedTypes)
            {
                var fields = def.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var field in fields)
                {
                    InputPortDesc desc = (InputPortDesc)field.GetCustomAttribute(typeof(InputPortDesc));

                    if (desc != null)
                    {
                        Port p = (Port)field.GetValue(null);
                        p.PortNumber = desc.InnerIndex;
                        p.WorkType = PortWorkType.Input;
                        p.Desc = desc;

                        inputPorts.Add(p.PortNumber, p);

                        InPortValue iv = new InPortValue();
                        iv.desc = desc;
                        iv.field = field;
                        iv.port = p;
                        structure.Inputs.Add(iv);
                    }
                }
            }

            foreach (var def in asm.DefinedTypes)
            {
                var fields = def.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var field in fields)
                {
                    OutputPortDesc desc = (OutputPortDesc)field.GetCustomAttribute(typeof(OutputPortDesc));

                    if (desc != null)
                    {
                        Port p = (Port)field.GetValue(null);
                        p.WorkType = PortWorkType.Output;
                        p.PortNumber = desc.InnerIndex;
                        p.Desc = desc;

                        outputPorts.Add(p.PortNumber, p);

                        OutPortValue ov = new OutPortValue();
                        ov.desc = desc;
                        ov.field = field;
                        ov.port = p;
                        structure.Outputs.Add(ov);
                    }
                }
            }

            foreach (var def in asm.DefinedTypes)
            {
                var methods = def.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var method in methods)
                {
                    Watcher desc = (Watcher)method.GetCustomAttribute(typeof(Watcher));

                    if (desc != null)
                    {
                        Port p;

                        if (!inputPorts.TryGetValue(desc.InnerIndex, out p))
                            outputPorts.TryGetValue(desc.InnerIndex, out p);

                        if (p != null)
                        {
                            WatcherValue act = new WatcherValue();
                            act.instance = null;
                            act.method = method;
                            act.innerPort = desc.InnerIndex;
                            mListeners.Add(p, act);

                            WatcherValue lv = new WatcherValue();
                            structure.InnerWatchers.Add(act);
                        }
                    }
                }
            }
        }

        [Watcher((int)ID.OptionLoaded)]
        static void OnExtensionsLoaded()
        {

        }

        static OutPortValue GetOutputPort(string asmout, string outputport)
        {
            foreach(var com in Components)
            {
                var ov = com.Value.Outputs.Find(i => i.field.Name == outputport && i.field.DeclaringType.Assembly.FullName
                  == asmout);
                if (ov != null)
                    return ov;
            }
            return null;
        }

        static InPortValue GetInputPort(string asmin, string inport)
        {
            foreach (var com in Components)
            {
                var ov = com.Value.Inputs.Find(i => i.field.Name == inport && i.field.DeclaringType.Assembly.FullName
                  == asmin);
                if (ov != null)
                    return ov;
            }
            return null;
        }

        public static void ChangePortTarget(string asmout,string outputport,string asmin,string inputport)
        {
            OutPortValue op = GetOutputPort(asmout, outputport);
            InPortValue ip = GetInputPort(asmin, inputport);

            foreach (var outin in mOutIns)
                outin.Value.RemoveAll(i => i == ip.port);

            PortCollection coellection;

            if (!mOutIns.TryGetValue(op.port, out coellection))
            {
                coellection = new PortCollection();
                mOutIns.Add(op.port, coellection);
            }
            coellection.Add(ip.port);
        }

        public static void Input(Port p)
        {
            if(p.WorkType.HasFlag(PortWorkType.Output))
            {
                PortCollection inputs;
                if (mOutIns.TryGetValue(p, out inputs))
                {
                    foreach (var listener in inputs)
                        listener.SetValue(p.RawValue);
                }
            }

            WatcherValue act;
            if (mListeners.TryGetValue(p, out act))
                act.method.Invoke(act.instance, null);
        }
    }
}
