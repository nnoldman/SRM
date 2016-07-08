using ATrigger;
using Core;
using Newtonsoft.Json;
using Shortcut;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

public partial class MainForm : Form
{
    public MainForm()
    {
        Center.Form = this;
        ATrigger.DataCenter.AddInstance(this);
        InitializeComponent();
        Center.ExtensionLoader.Load();
        PortHub.OnExtensionLoaded.Trigger();
        InitOptions();
        LoadOption();
        PortHub.OnOptionLoaded.Trigger();
        InitMenus();
        InitMenuButtons();
        InitShortKeys();
        LoadLayout();
        PortHub.OnLayoutEnd.Trigger();
        InitSkin();
        PortHub.OnInitialized.Trigger();
    }
    class MenuNode
    {
        public string content;
        public MethodInfo method;
        public List<MenuNode> children = new List<MenuNode>();
    }
    void AddShortCutFromType(Type tp)
    {
        MethodInfo[] methods = tp.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var method in methods)
        {
            var attr = method.GetCustomAttribute(typeof(AddShortCut));

            if (attr != null)
            {
                var menu = (AddShortCut)attr;
                if (Center.HotKeys.ContainsKey(method))
                    return;
                Center.HotKeys.Add(method, menu);
            }
        }
    }
    void AddMenuFromType(Type tp, MenuNode root)
    {
        MethodInfo[] methods = tp.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var method in methods)
        {
            var attr = method.GetCustomAttribute(typeof(AddMenu));

            if (attr != null)
            {
                var menu = (AddMenu)attr;

                var menus = menu.Text.Split('/');

                MenuNode parent = root;

                for (int i = 0; i < menus.Length; ++i)
                {
                    var str = menus[i];

                    var n = parent.children.Find((item) => item.content == str);

                    if (n != null)
                    {
                        parent = n;
                    }
                    else
                    {
                        MenuNode node = new MenuNode();
                        node.content = str;
                        if (i == menus.Length - 1)
                            node.method = method;
                        parent.children.Add(node);
                        parent = node;
                    }
                }
            }
        }
    }


    void AddShortCutFromASM(Assembly asm)
    {
        foreach (var extensionType in asm.DefinedTypes)
            AddShortCutFromType(extensionType.AsType());
    }
    void AddMenuFromASM(Assembly asm, MenuNode root)
    {
        foreach (var extensionType in asm.DefinedTypes)
            AddMenuFromType(extensionType.AsType(), root);
    }

    void AddOptionFromASM(Assembly asm)
    {
        foreach (var type in asm.DefinedTypes)
        {
            Attribute attr = type.GetCustomAttribute(typeof(AddOption));

            if (attr != null)
            {
                List<Type> typeList;
                AddOption option = (AddOption)attr;
                if (!Center.OptionTypes.TryGetValue(option.Cate, out typeList))
                {
                    typeList = new List<Type>();
                    Center.OptionTypes.Add(option.Cate, typeList);
                }
                if (!typeList.Contains(type))
                    typeList.Add(type);
            }
        }
    }

    void AddMunuButtonFromType(Type tp)
    {
        MethodInfo[] methods = tp.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

        foreach (var method in methods)
        {
            var attr = method.GetCustomAttribute(typeof(AddMenuButton));

            if (attr != null)
            {
                if (Center.MenuButtons.Keys.Contains(method))
                    continue;

                var menu = (AddMenuButton)attr;
                Center.MenuButtons.Add(method, menu);

                Image image = null;
                if (menu.LoaderType != null)
                {
                    MethodInfo loader = menu.LoaderType.GetMethod("GetImage", BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Static);
                    if (loader != null)
                        image = (Image)loader.Invoke(null, null);
                }
                ToolStripMenuItem item = new ToolStripMenuItem(menu.Text, image);
                item.Click += (sender, e) => method.Invoke(null, null);
                this.menuStrip1.Items.Add(item);
            }
        }
    }

    void AddMenuButtonsFromASM(Assembly asm)
    {
        foreach (var type in asm.DefinedTypes)
            AddMunuButtonFromType(type);
    }

    void InitMenuButtons()
    {
        foreach (var extensionType in Center.ExtensionLoader.Types)
            AddMenuButtonsFromASM(extensionType.Value.Assembly);
    }

    void InitOptions()
    {
        foreach (var extensionType in Center.ExtensionLoader.Types)
            AddOptionFromASM(extensionType.Value.Assembly);

        foreach (var childOption in Center.OptionTypes)
        {
            Core.Object child = Center.Option.FindChildByName(childOption.Key);
           
            if (!child)
            {
                child = new Core.Object();
                child.Name = childOption.Key;
                Center.Option.Children.Add(child);
            }

            foreach (var t in childOption.Value)
                child.AddComponent(t);
        }
    }

    void InitShortKeys()
    {
        foreach (var extensionType in Center.ExtensionLoader.Types)
            AddShortCutFromASM(extensionType.Value.Assembly);

        UnbindHotKeys();
        BindHotKeys();
    }
    void InitMenus()
    {
        MenuNode root = new MenuNode();

        foreach (var extensionType in Center.ExtensionLoader.Types)
            AddMenuFromASM(extensionType.Value.Assembly, root);

        if (root.children.Count > 0)
        {
            root.children.Sort(SortFunc);

            foreach (var child in root.children)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem();
                this.MainMenu.Items.Add(menu);
                AddMenus(menu, child);
            }
        }
    }

    void AddMenus(ToolStripMenuItem parent, MenuNode node)
    {
        if (!string.IsNullOrEmpty(node.content))
        {
            parent.Text = node.content;
            parent.Click += OnMenuClick;
            parent.Tag = node.method;
        }

        node.children.Sort(SortFunc);

        foreach (var child in node.children)
        {
            ToolStripMenuItem menu = new ToolStripMenuItem();
            parent.DropDownItems.Add(menu);
            AddMenus(menu, child);
        }
    }

    static int SortFunc(MenuNode item1, MenuNode item2)
    {
        return item1.content.CompareTo(item2.content);
    }

    void OnMenuClick(object sender, EventArgs e)
    {
        ToolStripMenuItem menu = (ToolStripMenuItem)sender;
        MethodInfo method = (MethodInfo)(menu.Tag);
        if (method != null)
            method.Invoke(null, null);
    }

    static JsonSerializerSettings mJsonSetting;
    static JsonSerializerSettings JsonSetting
    {
        get
        {
            if(mJsonSetting==null)
            {
                mJsonSetting = new JsonSerializerSettings();
                mJsonSetting.TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full;
                mJsonSetting.Formatting = Formatting.Indented;
                mJsonSetting.TypeNameHandling = TypeNameHandling.Auto;
            }
            return mJsonSetting;
        }
    }

    void LoadOption()
    {
        if (Center.Option.Base && Directory.Exists(BaseOption.OptionsFloder))
        {
            Center.Option.Children.Clear();

            var files = Directory.GetFiles(BaseOption.OptionsFloder);

            foreach (var file in files)
            {
                var obj = JsonConvert.DeserializeObject<Core.Object>(File.ReadAllText(file), JsonSetting);
                Center.Option.Children.Add(obj);
            }
        }
    }

    static void SaveOption()
    {
        Center.Form.DockerContainer.SaveAsXml(Center.Option.Base.LayoutFile);

        if (!Directory.Exists(BaseOption.OptionsFloder))
            Directory.CreateDirectory(BaseOption.OptionsFloder);

        foreach(var child in Center.Option.Children)
        {
            string content = JsonConvert.SerializeObject(child, JsonSetting);
            File.WriteAllText(Path.Combine(BaseOption.OptionsFloder, child.Name + ".json"), content);
        }
    }

    private IDockContent GetContentFromPersistString(string persistString)
    {
        PersistStringParser parser = new PersistStringParser();

        if (!parser.Parse(persistString))
            return null;
        string stype = parser["Type"];
        Type tp = null;
        Center.ExtensionLoader.Types.TryGetValue(stype, out tp);
        if (tp != null)
        {
            Extension contet = (Extension)tp.GetConstructor(Type.EmptyTypes).Invoke(null);
            if (contet != null)
            {
                if (!contet.LoadFromPersistString(parser))
                {
                    contet.Dispose();
                    contet = null;
                    return null;
                }
            }
            return contet;
        }
        return null;
    }
    void InitSkin()
    {
        AutoHideStripSkin autoHideSkin = new AutoHideStripSkin();
        autoHideSkin.DockStripGradient.StartColor = Color.AliceBlue;
        autoHideSkin.DockStripGradient.EndColor = Color.Blue;
        autoHideSkin.DockStripGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
        autoHideSkin.TabGradient.StartColor = SystemColors.Control;
        autoHideSkin.TabGradient.EndColor = SystemColors.ControlDark;
        autoHideSkin.TabGradient.TextColor = SystemColors.ControlText;

        this.DockerContainer.Skin.AutoHideStripSkin = autoHideSkin;

        DockPaneStripSkin stripSkin = new DockPaneStripSkin();
        stripSkin.DocumentGradient.ActiveTabGradient.StartColor = Color.FromArgb(255, 220, 230, 255);
        stripSkin.DocumentGradient.ActiveTabGradient.EndColor = Color.FromArgb(255, 220, 230, 255);
        stripSkin.ToolWindowGradient.ActiveCaptionGradient.StartColor = Color.FromArgb(255, 220, 230, 255);
        stripSkin.ToolWindowGradient.ActiveCaptionGradient.EndColor = Color.FromArgb(255, 220, 230, 255);

        this.DockerContainer.Skin.DockPaneStripSkin = stripSkin;

        this.DockerContainer.DockBackColor = Color.FromArgb(0, 220, 230, 255);
        //this.DockerContainer.BackgroundImage = Image.FromFile("ExampleWatermark.jpg");
        //this.DockerContainer.BackgroundImageLayout = ImageLayout.Stretch;
    }
    void LoadLayout()
    {
        if (Center.Option.Base)
            if (File.Exists(Center.Option.Base.LayoutFile))
            this.DockerContainer.LoadFromXml(Center.Option.Base.LayoutFile, GetContentFromPersistString);
    }

    static void BindHotKeys()
    {
        foreach (var hotkey in Center.HotKeys)
            Center.HotKeyBinder.Bind(hotkey.Value.DefaultModifiers, hotkey.Value.DefaultKey).To(() => hotkey.Key.Invoke(null, null));
    }

    static void UnbindHotKeys()
    {
        foreach (var hotkey in Center.HotKeys)
        {
            var hk = new Hotkey(hotkey.Value.DefaultModifiers, hotkey.Value.DefaultKey);
            if (Center.HotKeyBinder.IsHotkeyAlreadyBound(hk))
                Center.HotKeyBinder.Unbind(hk);
        }
    }

    [AddMenu("View(&V)/Option")]
    static void OnOpenView()
    {
        Center.OnViewObject.value = Center.Option;
    }

    [Watcher((int)ID.ApplicationExit)]
    static void OnExit()
    {
        UnbindHotKeys();
        SaveOption();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        PortHub.OnApplicationExit.Trigger();
        Center.Form = null;
        base.OnFormClosed(e);
    }


    [AddMenu("File(&F)/Exit(&E)")]
    [AddShortCut(ShortCutIndex.ExitApplication, Modifiers.None, Keys.Escape, "Exit")]
    static void ExitApplication()
    {
        Application.Exit();
    }

    void dlg_FileOk(object sender, CancelEventArgs e)
    {
        throw new NotImplementedException();
    }
}
