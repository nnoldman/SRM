using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace SolutionExplorer
{
    internal enum ID
    {
        None,
        NameChanged,
    }

    internal class PortHub
    {
        [OutputPortDesc((int)ID.NameChanged,Arg1 = Arg.Str)]
        internal static Port_String ClickNode = new Port_String();
    }
}
