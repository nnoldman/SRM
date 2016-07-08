
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
    }
}
