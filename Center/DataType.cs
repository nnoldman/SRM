using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum DataType : int
    {
        ApplicationInialized,
        ApplicationExit,
        LayoutEnd,
        OpenDocument,
        CloseDocument,
        ActiveDocument,
        ChangeDocumentName,
        View,
        ExtensionsLoaded,
        OpenFloder,
    }
}
