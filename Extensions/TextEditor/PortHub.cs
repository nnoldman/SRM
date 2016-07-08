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
        SaveDocument,
        CloseDocument,
        ShowDocument,
        ClickTitle,
    }

    internal class PortHub
    {
        [InputPortDesc((int)ID.OpenDocument, Arg1 = Arg.Str)]
        internal static Port_String OpenDocument = new Port_String();

        [InputPortDesc((int)ID.LayoutEnd, Arg1 = Arg.Signal)]
        internal static Port OnLayoutEnd = new Port();

        [InputPortDesc((int)ID.SaveDocument, Container = PortDataContainerType.Dict, Arg1 = Arg.Str, Arg2 = Arg.Str)]
        internal static Port_StringDic SaveDocumentParam = new Port_StringDic();

        [OutputPortDesc((int)ID.ClickTitle, Arg1 = Arg.Str)]
        internal static Port_String OnClickTitle = new Port_String();

        [InputPortDesc((int)ID.ShowDocument, Arg1 = Arg.Str)]
        internal static Port_String OnShowDocument = new Port_String();

        [OutputPortDesc((int)ID.CloseDocument, Arg1 = Arg.Str)]
        internal static Port_String OnCloseDocument = new Port_String();

        //[InputPortDesc(InnerIndex = (int)ID.NameChanged, Container = PortDataContainerType.Dict, Arg1 = Arg.Str)]
        //internal static Port_StringDic OnNameChanged = new Port_StringDic();
    }
}
