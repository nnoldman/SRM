using System;
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

        public static void Toggler(string name, DockPanel dockPanel)
        {
            Docker docker = null;
            Dockers.TryGetValue(name, out docker);

            if (docker == null)
            {
                docker = new Docker();
                docker.TabText = name;
                Dockers.Add(name, docker);
                docker.Show(dockPanel, DockState.Float);
            }
            else
            {
                if (!docker.Visible)
                    docker.Show(dockPanel);
                else
                    docker.Hide();
            }
        }
        public static void SetVisible(string name,DockPanel dockPanel,bool visible)
        {
            Docker docker = null;

            Dockers.TryGetValue(name,out docker);

            if(visible)
            {
                if (docker == null)
                {
                    docker = new Docker();
                    docker.TabText = name;
                    Dockers.Add(name,docker);
                    docker.Show(dockPanel, DockState.Float);
                }
            }
            else
            {
                if (docker != null)
                    docker.Visible = false;
            }
        }

        public Docker()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.ResumeLayout(false);
            this.CloseButton = true;
        }

        ~Docker()
        {
        }

        protected override void OnClosed(EventArgs e)
        {
            Dockers.Remove(this.TabText);
            base.OnClosed(e);
        }
    }
}
