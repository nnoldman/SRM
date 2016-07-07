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
    public class Center : ATrigger.IStaticEmitterContainer
    {
        public static MainForm Form { get; internal set; }
        public static Option Option = new Option();

        public static DocumentManager DocumentManager = new DocumentManager();
        internal static Shortcut.HotkeyBinder HotKeyBinder = new Shortcut.HotkeyBinder();
        internal static ExtensionLoader ExtensionLoader = new ExtensionLoader();

        internal static Dictionary<string, List<Type>> OptionTypes = new Dictionary<string, List<Type>>();
        internal static Dictionary<MethodInfo, AddShortCut> HotKeys = new Dictionary<MethodInfo, AddShortCut>();
        internal static Dictionary<MethodInfo, AddMenuButton> MenuButtons = new Dictionary<MethodInfo, AddMenuButton>();
        internal static Dictionary<string, Type> ExtensionTypes = new Dictionary<string, Type>();


        [TriggerEmmiter((int)DataType.ApplicationExit)]
        public static Emmiter OnExit = new Emmiter();//para:empty

        [TriggerEmmiter((int)DataType.ExtensionsLoaded)]
        public static Emmiter OnExtensionsLoaded = new Emmiter();

        [TriggerEmmiter((int)DataType.LayoutEnd)]
        public static Emmiter OnLayoutEnd = new Emmiter();//para:empty
        
        [TriggerEmmiter((int)DataType.ApplicationInialized)]
        public static Emmiter OnInitialized = new Emmiter();//para:empty

        [TriggerEmmiter((int)DataType.OpenDocument)]
        public static DataContainer<string> CurrentOpenDoucment = new DataContainer<string>();

        [TriggerEmmiter((int)DataType.CloseDocument)]
        public static DataContainer<string> CurrentCloseDoucment = new DataContainer<string>();

        [TriggerEmmiter((int)DataType.ActiveDocument)]
        public static DataContainer<string> ActiveDocument = new DataContainer<string>();

        [TriggerEmmiter((int)DataType.ChangeDocumentName)]
        public static Emmiter<string, string> OnChangeDocumentName = new Emmiter<string, string>();

        [TriggerEmmiter((int)DataType.OpenFloder)]
        public static DataContainer<string> OnOpenFloder = new DataContainer<string>();

        [TriggerEmmiter((int)DataType.SelectObject)]
        public static DataContainer<Object> OnSelectObject = new DataContainer<Object>();

        [TriggerEmmiter((int)DataType.ViewObject)]
        public static DataContainer<Object> OnViewObject = new DataContainer<Object>();

        [TriggerEmmiter((int)DataType.Console)]
        public static Emmiter<string> Console = new Emmiter<string>();

        [TriggerEmmiter((int)DataType.ConsoleClear)]
        public static Emmiter OnConsoleClear = new Emmiter();
        
        [TriggerEmmiter((int)DataType.BeginBuild)]
        public static Emmiter BeginBuild = new Emmiter();

        [TriggerEmmiter((int)DataType.EndBuild)]
        public static Emmiter EndBuild = new Emmiter();

        public static Emmiter<string> TheOpenDocument = new Emmiter<string>();
    }
}
