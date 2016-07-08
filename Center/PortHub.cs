using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Core
{
    internal enum ID
    {
        None,
        ExtensionsLoad,
        OptionLoaded,
        LayoutEnd,
        ApplicationInialized,
        ApplicationExit,
        Console,
        ConsoleClear,
        SelectObject,
        ViewObject,
        BeginBuild,
        EndBuild,
    }

    internal class PortHub
    {
        [OutputPortDesc((int)ID.ExtensionsLoad, Arg1 = Arg.Signal)]
        internal static Port OnExtensionLoaded = new Port();

        [OutputPortDesc((int)ID.OptionLoaded, Arg1 = Arg.Signal)]
        internal static Port OnOptionLoaded = new Port();

        [OutputPortDesc((int)ID.LayoutEnd)]
        public static Port OnLayoutEnd = new Port();

        [OutputPortDesc((int)ID.ApplicationInialized)]
        public static Port OnInitialized = new Port();

        [OutputPortDesc((int)ID.ApplicationExit, Arg1 = Arg.Signal)]
        internal static Port OnApplicationExit = new Port();

        [InputPortDesc((int)ID.ConsoleClear, Arg1 = Arg.Signal)]
        internal static Port OnConsoleClear = new Port();

        [InputPortDesc((int)ID.Console, Arg1 = Arg.Str)]
        internal static Port_String OnConsole = new Port_String();

        [OutputPortDesc((int)ID.BeginBuild, Arg1 = Arg.Signal)]
        internal static Port OnBeginBuild = new Port();

        [OutputPortDesc((int)ID.EndBuild, Arg1 = Arg.Signal)]
        internal static Port OnEndBuild = new Port();

        [InputPortDesc((int)ID.SelectObject, Arg1 = Arg.Object)]
        internal static Port_Object OnSelectObject = new Port_Object();

        [InputPortDesc((int)ID.ViewObject, Arg1 = Arg.Object)]
        internal static Port_Object OnViewObject = new Port_Object();
    }

}
