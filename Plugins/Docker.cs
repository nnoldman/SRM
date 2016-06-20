﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Plugins
{
    public class Docker : DockContent
    {
        static Dictionary<string, Docker> Dockers = new Dictionary<string, Docker>();

        public static void Toggler(string name, DockPanel dockPanel, Type tp = null)
        {
            Docker docker = null;

            Dockers.TryGetValue(name, out docker);

            if (docker == null)
            {
                if (tp != null)
                {
                    docker = (Docker)tp.GetConstructor(Type.EmptyTypes).Invoke(null);
                }
                docker.TabText = name;
                docker.Show(dockPanel, docker.IsDocument() ? DockState.Document : DockState.Float);
                Dockers.Add(name, docker);
            }
            else
            {
                if (!docker.Visible)
                    docker.Show(dockPanel);
                else
                    docker.Hide();
            }
        }

        protected virtual bool IsDocument()
        {
            return false;
        }

        public Docker()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float
                | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft
                | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight
                | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop
                | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom;
            if (this.IsDocument())
                this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.ResumeLayout(false);
            this.CloseButton = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Docker
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Docker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.ResumeLayout(false);

        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            Dockers.Remove(this.TabText);
        }
    }
}
