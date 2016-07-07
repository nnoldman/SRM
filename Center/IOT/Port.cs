using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum PortWorkType
    {
        Output,
        Input,
        Double,
    }
    public enum Arg
    {
        None,
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
    }
    public class OutputPort : PortDesc
    {
    }
    public class InputPort: PortDesc
    {
        public int InnerIndex;
    }
    public class Port
    {
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

    public class Listeners : List<Port> { };

    public class Port_String:Port
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
