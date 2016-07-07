using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class PortItem
    {
        internal string AsmName;
        internal string PortName;
    }
    internal class PortPair
    {
        internal PortItem Input;
        internal PortItem Target;
    }

    [AddOption("PortHub")]
    internal class PortOption : Component
    {
        internal List<PortPair> PortPairs = new List<PortPair>(); 
    }
}
