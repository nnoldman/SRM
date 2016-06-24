using ATrigger;
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

namespace Saber
{
    public partial class MainForm : Form
    {
        public static DockPanel MainDocker;
        private static HotkeyBinder mHotKeyBinder = new HotkeyBinder();
        private int mFileMenuChldrenCount = 0;

        public MainForm()
        {
            ATrigger.DataCenter.AddInstance(this);
            
            InitializeComponent();
            BindHotKeys();
            InitBase();
            InitInnerPlugins();
            //LoadOption();
            //LoadLayout();
            InitSkin();
        }

        void BindHotKeys()
        {
            //mHotKeyBinder.Bind(Modifiers.Control, Keys.S).To(TextEditor.SaveFile);
        }

        void InitBase()
        {
            mFileMenuChldrenCount = this.FileToolStripMenuItem.DropDownItems.Count;
            MainDocker = this.dockPanel1;
        }

        Dictionary<string, Type> mInnerPluginTypes = new Dictionary<string, Type>();

        void LoadHistry(List<string> histroy)
        {
            while (this.FileToolStripMenuItem.DropDownItems.Count > mFileMenuChldrenCount)
                this.FileToolStripMenuItem.DropDownItems.RemoveAt(this.FileToolStripMenuItem.DropDownItems.Count - 1);

            if (histroy.Count == 0)
                return;

            this.FileToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            this.FileToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem("Histroy"));

            for (int i = 0; i < histroy.Count; ++i)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = (i + 1).ToString() + " " + histroy[i];
                item.Click += OpenHistroyFile;
                this.FileToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        [ATrigger.DataReceiver((int)DataType.FileHistroyChange)]
        public void OnHistroyFileChanged()
        {
            LoadHistry(Option.Main.File.Histroy.value);
        }

        void LoadOption()
        {
            Option.Main = (Option)fastJSON.JSON.ToObject(File.ReadAllText(Option.FileName));

            LoadHistry(Option.Main.File.Histroy.value);
        }
        void SaveOption()
        {
            this.dockPanel1.SaveAsXml(Option.Main.LayoutFile);

            string content = fastJSON.JSON.ToNiceJSON(Option.Main, new fastJSON.JSONParameters());

            File.WriteAllText(Option.FileName, content);
        }
        void OpenHistroyFile(object sender, EventArgs e)
        {
            int pos = sender.ToString().IndexOf(' ');
            if (pos != -1)
                DocumentManager.CreateDocument(sender.ToString().Substring(pos + 1));
            else
                throw new Exception();
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            PersistStringParser parser = new PersistStringParser();
            
            if (!parser.Parse(persistString))
                return null;
            string stype = parser["Type"];
            Type tp = null;
            mInnerPluginTypes.TryGetValue(stype, out tp);
            if (tp != null)
            {
                Docker contet = (Docker)tp.GetConstructor(Type.EmptyTypes).Invoke(null);
                if (contet != null)
                    contet.LoadFromPersistString(parser);
                return contet;
            }
            throw new Exception();
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

            this.dockPanel1.Skin.AutoHideStripSkin = autoHideSkin;

            DockPaneStripSkin stripSkin = new DockPaneStripSkin();
            stripSkin.DocumentGradient.ActiveTabGradient.StartColor = Color.FromArgb(255, 220, 230, 255);
            stripSkin.DocumentGradient.ActiveTabGradient.EndColor = Color.FromArgb(255, 220, 230, 255);
            stripSkin.ToolWindowGradient.ActiveCaptionGradient.StartColor = Color.FromArgb(255, 220, 230, 255);
            stripSkin.ToolWindowGradient.ActiveCaptionGradient.EndColor = Color.FromArgb(255, 220, 230, 255);
			
            this.dockPanel1.Skin.DockPaneStripSkin = stripSkin;

            this.dockPanel1.DockBackColor = Color.FromArgb(0, 220, 230, 255);
            //this.dockPanel1.BackgroundImage = Image.FromFile("ExampleWatermark.jpg");
            //this.dockPanel1.BackgroundImageLayout = ImageLayout.Stretch;
        }
        void LoadLayout()
        {
            if (File.Exists(Option.Main.LayoutFile))
                this.dockPanel1.LoadFromXml(Option.Main.LayoutFile, GetContentFromPersistString);
        }
        public bool InitInnerPlugins()
        {
            var asm = Assembly.GetCallingAssembly();

            var ptype = typeof(Core.Extension);

            foreach (var tp in asm.GetTypes())
            {
                if (ptype.IsAssignableFrom(tp))
                {
                    if (tp.CustomAttributes != null)
                    {
                        var attributes = tp.GetCustomAttributes(typeof(PluginVersion), true);

                        if (attributes != null && attributes.Length > 0)
                        {
                            PluginVersion version = (PluginVersion)attributes[0];

                            ToolStripMenuItem item = new ToolStripMenuItem();
                            item.Text = version.Name;
                            item.Click += OnViewClick;
                            this.ViewToolStripMenuItem.DropDownItems.Add(item);
                            mInnerPluginTypes.Add(tp.FullName, tp);
                        }
                    }
                }
            }
            return true;
        }

        [DataReceiver((int)DataType.ApplicationExit)]
        void OnExit()
        {
            SaveOption();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                OnExit();
        }

        private void OnViewClick(object sender, EventArgs e)
        {
            string text = sender.ToString();
            Type tp = null;
            mInnerPluginTypes.TryGetValue(text, out tp);
            Docker.Toggler(sender.ToString(), this.dockPanel1, tp);
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            ATrigger.DataCenter.RemoveInstance(this);
            this.OnExit();
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                DocumentManager.CreateDocument(file);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Center.OnExit.Trigger();
            Application.Exit();
        }
    }
}
