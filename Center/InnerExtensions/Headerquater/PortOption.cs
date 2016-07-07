using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PortItem
    {
        public string AsmName;
        public string PortName;
    }
    public class PortPair
    {
        public PortItem Target = new PortItem();
        public PortItem Input = new PortItem();
    }

    [AddOption("PortHub")]
    public class PortOption : Component
    {
        public List<PortPair> PortPairs = new List<PortPair>();
    }
}
