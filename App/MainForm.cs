using Plugins;
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

namespace App
{
    public partial class MainForm : Form
    {
        public static DockPanel docker;

        public MainForm()
        {
            InitializeComponent();
            docker = this.dockPanel1;
            InitInnerPlugins();
            LoadLayout();
            InitSkin();
        }

        string mConfigFile = "567.xml";

        Dictionary<string, Type> mInnerPluginTypes = new Dictionary<string, Type>();

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
                    contet.TabText = parser["TabText"];
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
        }
        void LoadLayout()
        {
            if (File.Exists(mConfigFile))
                this.dockPanel1.LoadFromXml(mConfigFile, GetContentFromPersistString);
        }
        public bool InitInnerPlugins()
        {
            var asm = Assembly.GetCallingAssembly();

            var ptype = typeof(Plugins.Plugin);

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
        void OnExit()
        {
            this.dockPanel1.SaveAsXml(mConfigFile);
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
            this.OnExit();
        }
    }
}
