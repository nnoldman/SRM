﻿using ATrigger;
using Core;
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
        Center.HotKey.Bind(Modifiers.None, Keys.Escape).To(() => Application.Exit());

        ATrigger.DataCenter.AddInstance(this);

        InitializeComponent();

        Center.Form = this;

        BindHotKeys();
        LoadOption();
        ExtensionLoader.Instance.Load();
        InitMenus();
        LoadLayout();
        InitSkin();
        Center.OnInitialized.Trigger();
    }
    class MenuNode
    {
        public string content;
        public MethodInfo method;
        public List<MenuNode> children = new List<MenuNode>();
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
    void AddMenuFromASM(Assembly asm, MenuNode root)
    {
        foreach (var extensionType in asm.DefinedTypes)
            AddMenuFromType(extensionType.AsType(), root);
    }

    void InitMenus()
    {
        MenuNode root = new MenuNode();

        AddMenuFromASM(Assembly.GetCallingAssembly(), root);

        foreach (var extensionType in ExtensionLoader.Instance.Types)
            AddMenuFromASM(extensionType.Value.Assembly, root);

        if (root.children.Count > 0)
        {
            root.children.Sort(SortFunc);

            foreach (var child in root.children)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem();
                this.MainMenu.Items.Add(menu);
                AddMenu(menu, child);
            }
        }
    }

    void AddMenu(ToolStripMenuItem parent, MenuNode node)
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
            AddMenu(menu, child);
        }
    }

    static int SortFunc(MenuNode item1, MenuNode item2)
    {
        return item1.content.CompareTo(item2.content);
    }

    void OnMenuClick(object sender, EventArgs e)
    {
        ToolStripMenuItem menu=(ToolStripMenuItem)sender;
        MethodInfo method = (MethodInfo)(menu.Tag);
        if (method != null)
            method.Invoke(null, null);
    }

    void BindHotKeys()
    {
        //mHotKeyBinder.Bind(Modifiers.Control, Keys.S).To(TextEditor.SaveFile);
    }

    void LoadOption()
    {
        if (File.Exists(Option.FileName))
            Center.Option = (Option)fastJSON.JSON.ToObject(File.ReadAllText(Option.FileName));
    }

    void SaveOption()
    {
        this.DockerContainer.SaveAsXml(Center.Option.LayoutFile);

        string content = fastJSON.JSON.ToNiceJSON(Center.Option, new fastJSON.JSONParameters());

        File.WriteAllText(Option.FileName, content);
    }


    private IDockContent GetContentFromPersistString(string persistString)
    {
        PersistStringParser parser = new PersistStringParser();

        if (!parser.Parse(persistString))
            return null;
        string stype = parser["Type"];
        Type tp = null;
        ExtensionLoader.Instance.Types.TryGetValue(stype, out tp);
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
        if (File.Exists(Center.Option.LayoutFile))
            this.DockerContainer.LoadFromXml(Center.Option.LayoutFile, GetContentFromPersistString);
        Center.OnLayoutEnd.Trigger();
    }

    [Receiver((int)DataType.ApplicationExit)]
    void OnExit()
    {
        SaveOption();
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
            OnExit();
    }

    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
        ATrigger.DataCenter.RemoveInstance(this);
        this.OnExit();
    }
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        Center.Form = null;
        base.OnFormClosed(e);
    }


    [AddMenu("File(&F)/Exit(&E)")]
    [AddShortKey(ShortCutIndex.ExitApplication, Modifiers.None, Keys.Escape, "Exit")]
    static void ExitApplication()
    {
        Center.OnExit.Trigger();
        Application.Exit();
    }

    void dlg_FileOk(object sender, CancelEventArgs e)
    {
        throw new NotImplementedException();
    }
}