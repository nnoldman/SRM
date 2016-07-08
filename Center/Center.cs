
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
    public class Center
    {
        public static MainForm Form { get; internal set; }
        public static Option Option = new Option();

        internal static Shortcut.HotkeyBinder HotKeyBinder = new Shortcut.HotkeyBinder();
        internal static ExtensionLoader ExtensionLoader = new ExtensionLoader();

        internal static Dictionary<string, List<Type>> OptionTypes = new Dictionary<string, List<Type>>();
        internal static Dictionary<MethodInfo, AddShortCut> HotKeys = new Dictionary<MethodInfo, AddShortCut>();
        internal static Dictionary<MethodInfo, AddMenuButton> MenuButtons = new Dictionary<MethodInfo, AddMenuButton>();
        internal static Dictionary<string, Type> ExtensionTypes = new Dictionary<string, Type>();

        //public static DataContainer<string> CurrentOpenDoucment = new DataContainer<string>();

        //[TriggerEmmiter((int)ID.CloseDocument)]
        //public static DataContainer<string> CurrentCloseDoucment = new DataContainer<string>();

        //[TriggerEmmiter((int)ID.ActiveDocument)]
        //public static DataContainer<string> ActiveDocument = new DataContainer<string>();

        //[TriggerEmmiter((int)ID.SelectObject)]
        //public static DataContainer<Object> OnSelectObject = new DataContainer<Object>();

        //[TriggerEmmiter((int)ID.ViewObject)]
        //public static DataContainer<Object> OnViewObject = new DataContainer<Object>();

        //[TriggerEmmiter((int)ID.Console)]
        //public static Emmiter<string> Console = new Emmiter<string>();

        //[TriggerEmmiter((int)ID.ConsoleClear)]
        //public static Emmiter OnConsoleClear = new Emmiter();
        
        //[TriggerEmmiter((int)ID.BeginBuild)]
        //public static Emmiter BeginBuild = new Emmiter();

        //[TriggerEmmiter((int)ID.EndBuild)]
        //public static Emmiter EndBuild = new Emmiter();

        //public static Emmiter<string> TheOpenDocument = new Emmiter<string>();
    }
}
