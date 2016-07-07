using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace TextEditor
{
    public enum ID
    {
        None,
        NameChanged,
    }

    internal class Interface
    {
        [OutputPort(Arg1 = Arg.Str)]
        internal static Port_String OpenDocument =new Port_String();

        [InputPort(InnerIndex = (int)ID.NameChanged)]
        internal static Port_String OnNameChanged = new Port_String();
    }
}
