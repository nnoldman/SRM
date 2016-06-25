﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using WeifenLuo.WinFormsUI.Docking;

namespace Saber
{
    [Core.ExtensionVersion(Name = "Extensions")]
    public partial class ExtensionsManager : Extension, ATrigger.ITriggerStatic
    {
        static ExtensionsManager Instance;

        public ExtensionsManager()
        {
            ATrigger.DataCenter.AddInstance(this);

            InitializeComponent();

            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;

            ShowContent();
        }

        protected override void OnFormClosed(System.Windows.Forms.FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            ATrigger.DataCenter.RemoveInstance(this);
            Instance = null;
        }

        [ATrigger.Receiver((int)DataType.ExtensionsLoaded)]
        public void ShowContent()
        {
            this.dataGridView1.Rows.Clear();

            foreach (var item in ExtensionLoader.Instance.Types)
            {
                int cnt = this.dataGridView1.Rows.Add();
                this.dataGridView1[0, cnt].Value = item.Key;
                this.dataGridView1[1, cnt].Value = item.Value.Module;
            }
        }

        [ATrigger.Receiver((int)DataType.View)]
        static void OnViewChange()
        {
            if (Center.View.Arg<Type>(0) == typeof(ExtensionsManager))
            {
                if (Instance == null)
                {
                    Instance = new ExtensionsManager();
                    Instance.TabText = "ExtensionsManager";
                    Instance.Show(Center.Container, DockState.Float);
                }
                else
                {
                    Instance.Hide();
                    Instance.Dispose();
                    Instance = null;
                }
            }
        }
    }
}
