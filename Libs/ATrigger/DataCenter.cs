using System.Collections;
using System;
using System.Reflection;
using System.Collections.Generic;
using DataType = System.Int32;
namespace ATrigger
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class Receiver : Attribute
    {
        public DataType dataType;

        public Receiver(DataType dataType)
        {
            this.dataType = dataType;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class Emmiter : Attribute
    {
        public DataType dataType;
        public Emmiter(int dataType)
        {
            this.dataType = dataType;
        }
    }
    public class DataCenter
    {
        static Dictionary<DataType, List<ReceiverData>> mReceivers = new Dictionary<DataType, List<ReceiverData>>();
        public static DataType CurrentType;
        static Stack<object> mParamStack = new Stack<object>();

        public static void InstallStaticTriggers(Assembly asm)
        {
            if (asm != null)
            {
                foreach (var tp in asm.DefinedTypes)
                {
                    if (typeof(ATrigger.ITriggerStatic).IsAssignableFrom(tp))
                    {
                        ATrigger.DataCenter.ProcessStaticTrigger(tp);
                    }
                }
            }
        }

        static void ProcessStaticTrigger(Type tp)
        {
            AddReceiversStatic(tp);
            AddEntityStatic(tp);
        }

        public static void AddInstance(object instance)
        {
            AddEntityWithInstance(instance);
            AddReceiversWithInstance(instance);
        }
        public static void RemoveInstance(object instance)
        {
            RemoveEntityWithInstance(instance);
            RemoveReceiversWithInstance(instance);
        }
        internal static void AddEntityStatic(Type tp)
        {
            FieldInfo[] fields = tp.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (fields != null && fields.Length > 0)
            {
                foreach (var field in fields)
                {
                    if (field.IsStatic)
                    {
                        object[] objs = field.GetCustomAttributes(typeof(Emmiter), true);
                        if (objs != null && objs.Length > 0)
                        {
                            Signal data = (Signal)field.GetValue(null);
                            if (data == null)
                            {
                                string warnstr = string.Format("Warning : DataEntity({0}.{1}) is null when scan type!", tp.Name, field.Name);
                                Logger.warning(warnstr);
                            }
                            else
                            {
                                data.dataType = ((Emmiter)objs[0]).dataType;

                                Entity entity = new Entity();
                                entity.instance = null;
                                entity.data = data;
                                mDataEntities.Add(data.dataType, entity);
                            }
                        }
                    }
                }
            }
        }
        internal static void AddReceiver(ReceiverData act)
        {
            List<ReceiverData> acts;

            if (!mReceivers.TryGetValue(act.type, out acts))
            {
                acts = new List<ReceiverData>();
                mReceivers.Add(act.type, acts);
            }
            acts.Add(act);
        }

        internal static void RemoveReceiver(DataType type, object instance)
        {
            List<ReceiverData> acts;

            if (mReceivers.TryGetValue(type, out acts))
            {
                acts.RemoveAll((item) => item.call.instance == instance);
            }
        }
        internal static object[] GetParams()
        {
            if (mParamStack.Count > 0)
                return (object[])mParamStack.Peek();
            return null;
        }
        internal static T GetParamByIndex<T>(int idx)
        {
            object[] paras = (object[])mParamStack.Peek();
            return paras != null ? (T)paras[idx] : default(T);
        }
        public static object CurrentParam
        {
            get
            {
                return mParamStack.Peek();
            }
            set
            {
                mParamStack.Push(value);
            }
        }

        internal static void Emitter(int dataType, params object[] args)
        {
            CurrentType = dataType;
            CurrentParam = args;

            List<ReceiverData> acts;

            if (mReceivers.TryGetValue(dataType, out acts))
            {
                for (int i = 0; i < acts.Count; ++i)
                {
                    var act = acts[i];
                    if (act != null)
                    {
                        //act.call.method.Invoke(act.call.instance, args);
                        act.call.method.Invoke(act.call.instance, null);
                    }
                }
            }
            mParamStack.Pop();
        }

        internal class ReceiverData
        {
            public DataType type = Signal.InvalidDataType;
            public CallCack call = new CallCack();
        }
        internal class CallCack
        {
            public MethodInfo method;
            public object instance;
        }
        internal class Entity
        {
            public object instance;
            public Signal data;
        }
        static Dictionary<DataType, Entity> mDataEntities = new Dictionary<DataType, Entity>();

        internal static void RemoveEntityWithInstance(object instance)
        {
            List<DataType> garbages = new List<DataType>();

            foreach (var d in mDataEntities)
            {
                if (d.Value.instance == instance)
                {
                    garbages.Add(d.Key);
                }
            }
            foreach (var g in garbages)
            {
                mDataEntities.Remove(g);
            }
        }
        internal static void AddEntityWithInstance(object instance)
        {
            Type tp = instance.GetType();
            Type baseType = typeof(Signal);

            FieldInfo[] fields = tp.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (fields != null && fields.Length > 0)
            {
                foreach (var field in fields)
                {
                    if (!field.IsStatic)
                    {
                        object[] objs = field.GetCustomAttributes(typeof(Emmiter), true);
                        if (objs != null && objs.Length > 0)
                        {
                            try
                            {
                                if (field.FieldType.IsSubclassOf(baseType) || field.FieldType == baseType)
                                {
                                    Signal data = (Signal)field.GetValue(instance);
                                    if (data == null)
                                    {
                                        string warnstr = string.Format("Warning : DataEntity({0}.{1}) is null when class construct!", tp.Name, field.Name);
                                        Logger.warning(warnstr);
                                    }
                                    else
                                    {
                                        data.dataType = ((Emmiter)objs[0]).dataType;
                                        if (!mDataEntities.ContainsKey(data.dataType))
                                        {
                                            Entity entity = new Entity();
                                            entity.instance = instance;
                                            entity.data = data;
                                            mDataEntities.Add(data.dataType, entity);
                                        }
                                    }
                                }
                                else
                                {
                                    string warnstr = string.Format("Warning : DataEntity({0}.{1}) is invalid, field must be child class of AData", tp.Name, field.Name);
                                    Logger.error(warnstr);
                                }
                            }
                            catch (Exception exc)
                            {
                                Logger.error(exc.Message + " Field Error!(" + field.Name + ")");
                            }
                        }
                    }
                }
            }
        }


        internal static void AddReceiversStatic(Type tp)
        {
            MethodInfo[] methods = tp.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            if (methods != null && methods.Length > 0)
            {
                foreach (var method in methods)
                {
                    //var objs = method.GetCustomAttributesData();

                    //foreach (var obj in objs)
                    //{
                    //    //Receiver attr = (Receiver)obj;
                    //    //ReceiverData receiver = new ReceiverData();
                    //    //receiver.call.instance = null;
                    //    //receiver.call.method = method;
                    //    //receiver.type = attr.dataType;
                    //    //DataCenter.AddReceiver(receiver);
                    //}

                    object[] objs = method.GetCustomAttributes(typeof(Receiver), false);

                    if (objs != null && objs.Length > 0)
                    {

                        foreach (var obj in objs)
                        {
                            Receiver attr = (Receiver)obj;
                            ReceiverData receiver = new ReceiverData();
                            receiver.call.instance = null;
                            receiver.call.method = method;
                            receiver.type = attr.dataType;
                            DataCenter.AddReceiver(receiver);
                        }
                    }
                }
            }
        }


        internal class ReceiverMeta
        {
            public MethodInfo method;
            public object[] attributes;
        }

        static Dictionary<Type, List<ReceiverMeta>> mReceiverMetas = new Dictionary<Type, List<ReceiverMeta>>();

        internal static void AddReceiversWithInstance(object instance)
        {
            Type tp = instance.GetType();

            List<ReceiverMeta> receiveList = null;
            if (!mReceiverMetas.TryGetValue(tp, out receiveList))
            {
                receiveList = new List<ReceiverMeta>();
                mReceiverMetas.Add(tp, receiveList);

                MethodInfo[] methods = tp.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                if (methods != null && methods.Length > 0)
                {
                    foreach (var method in methods)
                    {
                        object[] objs = method.GetCustomAttributes(typeof(Receiver), true);

                        if (objs != null && objs.Length > 0)
                        {
                            ReceiverMeta meta = new ReceiverMeta();
                            meta.method = method;
                            meta.attributes = objs;
                            receiveList.Add(meta);
                        }
                    }
                }
            }

            if (receiveList.Count > 0)
            {
                for (int i = 0; i < receiveList.Count; ++i)
                {
                    var meta = receiveList[i];

                    foreach (var obj in meta.attributes)
                    {
                        Receiver attr = (Receiver)obj;
                        ReceiverData receiver = new ReceiverData();
                        receiver.call.instance = instance;
                        receiver.call.method = meta.method;
                        receiver.type = attr.dataType;
                        DataCenter.AddReceiver(receiver);
                    }
                }
            }
        }
        internal static void RemoveReceiversWithInstance(object instance)
        {
            Type tp = instance.GetType();

            MethodInfo[] methods = tp.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (methods != null && methods.Length > 0)
            {
                foreach (var method in methods)
                {
                    if (!method.IsStatic)
                    {
                        object[] objs = method.GetCustomAttributes(typeof(Receiver), true);

                        if (objs != null && objs.Length > 0)
                        {
                            foreach (var obj in objs)
                            {
                                Receiver attr = (Receiver)obj;
                                DataCenter.RemoveReceiver(attr.dataType, instance);
                            }
                        }
                    }
                }
            }
        }
    }
}
