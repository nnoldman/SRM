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
    }

    internal class PortHub
    {
        [OutputPortDesc((int)ID.ExtensionsLoad, Arg1 = Arg.Signal)]
        internal static Pin_Signal OnExtensionLoaded = new Pin_Signal();

        [OutputPortDesc((int)ID.OptionLoaded, Arg1 = Arg.Signal)]
        internal static Pin_Signal OnOptionLoaded = new Pin_Signal();


        [OutputPortDesc((int)DataType.LayoutEnd)]
        public static Pin_Signal OnLayoutEnd = new Pin_Signal();

        [OutputPortDesc((int)DataType.ApplicationInialized)]
        public static Pin_Signal OnInitialized = new Pin_Signal();

        [OutputPortDesc((int)ID.ApplicationExit, Arg1 = Arg.Signal)]
        internal static Pin_Signal OnApplicationExit = new Pin_Signal();
    }
}
