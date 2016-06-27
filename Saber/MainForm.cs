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
        private static HotkeyBinder mHotKeyBinder = new HotkeyBinder();
        private int mFileMenuChldrenCount = 0;

        public MainForm()
        {
            Center.HotKey.Bind(Modifiers.None, Keys.Escape).To(() => Application.Exit());

            ATrigger.DataCenter.AddInstance(this);
            ATrigger.DataCenter.InstallStaticTriggers(typeof(Core.Center).Assembly);

            InitializeComponent();
            BindHotKeys();
            InitBase();
            LoadOption();
            InitExtensions();
            LoadLayout();
            InitSkin();
            Center.OnInitialized.Trigger();
        }

        void BindHotKeys()
        {
            //mHotKeyBinder.Bind(Modifiers.Control, Keys.S).To(TextEditor.SaveFile);
        }

        void InitBase()
        {
            mFileMenuChldrenCount = this.FileToolStripMenuItem.DropDownItems.Count;
            Center.Container = this.dockPanel1;
        }

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

        [ATrigger.Receiver((int)DataType.OpenDocument)]
        public void OnDocumentOpen()
        {
            if (Center.Option.File.Histroy.Remove(Center.CurrentOpenDoucment.value))
                LoadHistry(Center.Option.File.Histroy);
        }

        [ATrigger.Receiver((int)DataType.CloseDocument)]
        public void OnDocumentClose()
        {
            while (Center.Option.File.Histroy.Count >= Center.Option.File.MaxHistroyCount)
                Center.Option.File.Histroy.RemoveAt(0);
            Center.Option.File.Histroy.Add(Center.CurrentCloseDoucment.value);
            LoadHistry(Center.Option.File.Histroy);
        }

        void LoadOption()
        {
            if (File.Exists(Option.FileName))
            {
                Center.Option = (Option)fastJSON.JSON.ToObject(File.ReadAllText(Option.FileName));
                LoadHistry(Center.Option.File.Histroy);
            }
        }
        void SaveOption()
        {
            this.dockPanel1.SaveAsXml(Center.Option.LayoutFile);

            string content = fastJSON.JSON.ToNiceJSON(Center.Option, new fastJSON.JSONParameters());

            File.WriteAllText(Option.FileName, content);
        }
        void OpenHistroyFile(object sender, EventArgs e)
        {
            int pos = sender.ToString().IndexOf(' ');
            if (pos != -1)
                Center.CurrentOpenDoucment.value = sender.ToString().Substring(pos + 1);
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
            if (File.Exists(Center.Option.LayoutFile))
                this.dockPanel1.LoadFromXml(Center.Option.LayoutFile, GetContentFromPersistString);
            Center.OnLayoutEnd.Trigger();
        }
        public bool InitExtensions()
        {
            ExtensionLoader.Instance.Load();

            foreach (var extension in ExtensionLoader.Instance.Types)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = extension.Key;
                item.Click += OnViewClick;
                this.ViewToolStripMenuItem.DropDownItems.Add(item);
            }
            return true;
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

        private void OnViewClick(object sender, EventArgs e)
        {
            string text = sender.ToString();
            Type tp = null;
            ExtensionLoader.Instance.Types.TryGetValue(text, out tp);
            Center.View.Trigger(tp);
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
                Center.CurrentOpenDoucment.value = file;
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

        private void OpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            var result = dlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Center.OpenFloder.value = dlg.SelectedPath;
            }
        }

        void dlg_FileOk(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
