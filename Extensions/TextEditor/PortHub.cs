using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace TextEditor
{
    internal enum ID
    {
        None,
        OpenDocument,
        LayoutEnd,
    }

    internal class PortHub
    {
        [InputPortDesc((int)ID.OpenDocument, Arg1 = Arg.Str)]
        internal static Port_String OpenDocument = new Port_String();

        [InputPortDesc((int)ID.LayoutEnd, Arg1 = Arg.Signal)]
        internal static Port_String OnLayoutEnd = new Port_String();

        //[InputPortDesc(InnerIndex = (int)ID.NameChanged, Container = PortDataContainerType.Dict, Arg1 = Arg.Str)]
        //internal static Port_StringDic OnNameChanged = new Port_StringDic();
    }
}
