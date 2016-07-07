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
        static Dictionary<Port, PortConnection> mOutIns = new Dictionary<Port, PortConnection>();
        static Dictionary<Port, IntputListenerValue> mListeners = new Dictionary<Port, IntputListenerValue>();

        internal static void OnLoadAssambly(Assembly asm)
        {
            if (Components.Keys.Contains(asm))
                return;

            Constructure structure = new Constructure();
            Components.Add(asm, structure);

            Dictionary<int, Port> inputPorts = new Dictionary<int, Port>();

            foreach(var def in asm.DefinedTypes)
            {
                var fields = def.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var field in fields)
                {
                    InputPortDesc desc = (InputPortDesc)field.GetCustomAttribute(typeof(InputPortDesc));

                    if (desc != null)
                    {
                        Port p = (Port)field.GetValue(null);
                        p.PortNumber = desc.InnerIndex;

                        inputPorts.Add(p.PortNumber, p);

                        InPortValue iv = new InPortValue();
                        iv.desc = desc;
                        iv.field = field;
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
                        OutPortValue ov = new OutPortValue();
                        ov.desc = desc;
                        ov.field = field;
                        structure.Outputs.Add(ov);
                    }
                }
            }

            foreach (var def in asm.DefinedTypes)
            {
                var methods = def.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var method in methods)
                {
                    InputListener desc = (InputListener)method.GetCustomAttribute(typeof(InputListener));

                    if (desc != null)
                    {

                        Port p;
                        if(inputPorts.TryGetValue(desc.InnerIndex,out p))
                        {
                            IntputListenerValue act = new IntputListenerValue();
                            act.instance = null;
                            act.method = method;
                            act.innerPort = desc.InnerIndex;
                            mListeners.Add(p, act);

                            IntputListenerValue lv = new IntputListenerValue();
                            structure.InputListeners.Add(act);
                        }
                    }
                }
            }
        }

        public static void Input(Port p)
        {
            if(p.WorkType.HasFlag(PortWorkType.Output))
            {
                PortConnection inputs;
                if (mOutIns.TryGetValue(p, out inputs))
                {
                    foreach (var listener in inputs)
                        listener.SetValue(p.RawValue);
                }
            }
            else if (p.WorkType.HasFlag(PortWorkType.Input))
            {
                IntputListenerValue act;
                if (mListeners.TryGetValue(p, out act))
                    act.method.Invoke(act.instance, null);
            }
        }
    }
}
