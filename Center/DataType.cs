﻿using System;
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
        ExtensionsLoaded,
        LayoutEnd,
        OpenDocument,
        CloseDocument,
        ActiveDocument,
        ChangeDocumentName,
        OpenFloder,
        SelectObject,
        Console,
        ConsoleClear,
        BeginBuild,
        EndBuild,
    }
}
