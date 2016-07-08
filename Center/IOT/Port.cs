using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum PortWorkType
    {
        None,
        Output = 1,
        Input = 2,
        Double = 4 | Output | Input,
    }
    public enum Arg
    {
        None,
        Signal,
        Bool,
        Str,
        Number,
        Bytes,
    }
    public enum PortDataContainerType
    {
        Dict,
        List,
    }
    public class PortDesc : Attribute
    {
        public PortDataContainerType Container;
        public Arg Arg1;
        public Arg Arg2;
        public string Desc;
        public int InnerIndex;
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class OutputPortDesc : PortDesc
    {
        public OutputPortDesc(int portNumber)
        {
            this.InnerIndex = portNumber;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class InputPortDesc : PortDesc
    {
        public InputPortDesc(int portNumber)
        {
            this.InnerIndex = portNumber;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class Watcher : Attribute
    {
        public Watcher(int portNumber)
        {
            InnerIndex = portNumber;
        }
        internal int InnerIndex;
    }

    public class Port
    {
        internal int PortNumber=0;
        internal PortWorkType WorkType = PortWorkType.None;
        internal PortDesc Desc;

        protected object mValue;

        public object RawValue
        {
            get { return mValue; }
        }
        public void SetValue(object v)
        {
            mValue = v;
            Bus.Input(this);
        }
    }

    public class PortCollection : List<Port> { };

    public class Pin_Signal:Port
    {
        public void Trigger()
        {
            Bus.Input(this);
        }
    }

    public class Port_String : Port
    {
        public Port_String()
        {
            mValue = string.Empty;
        }
        public string Value
        {
            get
            {
                return (string)mValue;
            }
            set
            {
                mValue = value;
                Bus.Input(this);
            }
        }
    }

    public class Port_StringDic : Port
    {
        public Port_StringDic()
        {
            mValue = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Value
        {
            get
            {
                return (Dictionary<string, string>)mValue;
            }
            set
            {
                mValue = value;
                Bus.Input(this);
            }
        }
    }

    public class Port_Bool : Port
    {
        public Port_Bool()
        {
            mValue = false;
        }
        public bool Value
        {
            get
            {
                return (bool)mValue;
            }
            set
            {
                mValue = value;
                Bus.Input(this);
            }
        }
    }

    public class Port_Int : Port
    {
        public Port_Int()
        {
            mValue = -1;
        }

        public int Value
        {
            get
            {
                return (int)mValue;
            }
            set
            {
                mValue = value;
                Bus.Input(this);
            }
        }
    }
}
