using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class IntputListenerValue
    {
        internal object instance;
        internal MethodInfo method;
        internal int innerPort;
    }

    internal class InPortValue
    {
        internal FieldInfo field;
        internal InputPortDesc desc;
    }

    internal class OutPortValue
    {
        internal FieldInfo field;
        internal OutputPortDesc desc;
    }

    internal class Constructure
    {
        internal List<InPortValue> Inputs = new List<InPortValue>();
        internal List<OutPortValue> Outputs = new List<OutPortValue>();
        internal List<IntputListenerValue> InputListeners = new List<IntputListenerValue>();
    }
}
