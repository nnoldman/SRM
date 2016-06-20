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
        public MainForm()
        {
            InitializeComponent();
            InitInnerPlugins();
            LoadLayout();
        }

        string mConfigFile = "567.xml";

        Dictionary<string, Type> mInnerPluginTypes = new Dictionary<string, Type>();

        private IDockContent GetContentFromPersistString(string persistString)
        {
            Type tp = null;
            mInnerPluginTypes.TryGetValue(persistString, out tp);
            if (tp != null)
            {
                IDockContent contet = (IDockContent)tp.GetConstructor(Type.EmptyTypes).Invoke(null);
                if (contet != null)
                {
                    contet.DockHandler.TabText=persistString;
                }
                return contet;
            }
            //if (persistString == typeof(Explorer).FullName)
            //{
            //    return new Explorer();
            //}
            //else
            //{
            //    //// DummyDoc overrides GetPersistString to add extra information into persistString.
            //    //// Any DockContent may override this value to add any needed information for deserialization.

            //    //string[] parsedStrings = persistString.Split(new char[] { ',' });
            //    //if (parsedStrings.Length != 3)
            //    //    return null;

            //    //if (parsedStrings[0] != typeof(DummyDoc).ToString())
            //    //    return null;

            //    //DummyDoc dummyDoc = new DummyDoc();
            //    //if (parsedStrings[1] != string.Empty)
            //    //    dummyDoc.FileName = parsedStrings[1];
            //    //if (parsedStrings[2] != string.Empty)
            //    //    dummyDoc.Text = parsedStrings[2];

            //    //return dummyDoc;
            //}
            throw new Exception();
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

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            OnExit();
        }
    }
}
