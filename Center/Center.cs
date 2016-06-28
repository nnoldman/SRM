using ATrigger;
using Shortcut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Core
{
    public class Center : ATrigger.ITriggerStatic
    {
        public static MainForm Form { get; internal set; }

        public static Option Option = new Option();
        public static DocumentManager DocumentManager = new DocumentManager();
        public static Shortcut.HotkeyBinder HotKeyBinder = new Shortcut.HotkeyBinder();
        public static ExtensionLoader ExtensionLoader = new ExtensionLoader();

        internal static Dictionary<MethodInfo, AddShortCut> HotKeys = new Dictionary<MethodInfo, AddShortCut>();
        internal static Dictionary<string, Type> ExtensionTypes = new Dictionary<string, Type>();

        [Emmiter((int)DataType.ApplicationExit)]
        public static Signal OnExit = new Signal();//para:empty

        [Emmiter((int)DataType.LayoutEnd)]
        public static Signal OnLayoutEnd = new Signal();//para:empty
        
        [Emmiter((int)DataType.ApplicationInialized)]
        public static Signal OnInitialized = new Signal();//para:empty

        [Emmiter((int)DataType.OpenDocument)]
        public static ATrigger<string> CurrentOpenDoucment = new ATrigger<string>();

        [Emmiter((int)DataType.CloseDocument)]
        public static ATrigger<string> CurrentCloseDoucment = new ATrigger<string>();

        [Emmiter((int)DataType.ActiveDocument)]
        public static ATrigger<string> ActiveDocument = new ATrigger<string>();

        [Emmiter((int)DataType.ChangeDocumentName)]
        public static Signal OnChangeDocumentName = new Signal();//para:old name,new name

        [Emmiter((int)DataType.ExtensionsLoaded)]
        public static Signal ExtensionsLoaded = new Signal();

        [Emmiter((int)DataType.OpenFloder)]
        public static ATrigger<string> OpenFloder = new ATrigger<string>();
    }
}
