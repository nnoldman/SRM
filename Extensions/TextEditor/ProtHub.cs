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

    internal class ProtHub
    {
        [OutputPortDesc(Arg1 = Arg.Str)]
        internal static Port_String OpenDocument =new Port_String();

        [InputPortDesc(InnerIndex = (int)ID.NameChanged, Container = PortDataContainerType.Dict, Arg1 = Arg.Str)]
        internal static Port_StringDic OnNameChanged = new Port_StringDic();
    }
}
