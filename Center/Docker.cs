using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Core
{
    public class Extension : DockContent
    {
        static Dictionary<string, Extension> Dockers = new Dictionary<string, Extension>();

        public static void Toggler(string name, DockPanel dockPanel, Type tp = null)
        {
            Extension docker = null;

            Dockers.TryGetValue(name, out docker);

            if (docker == null)
            {
                ConstructorInfo construtor = tp.GetConstructor(Type.EmptyTypes);
                docker = (Extension)(construtor.Invoke(null));
                docker.TabText = name;
                docker.Show(dockPanel, docker.IsDocument() ? DockState.Document : DockState.Float);
                Dockers.Add(name, docker);
            }
            else
            {
                if (!docker.Visible)
                {
                    docker.Show(dockPanel);
                }
                else
                {
                    Dockers.Remove(name);
                    docker.Hide();
                    docker.Dispose();
                    docker = null;
                }
            }
        }

        protected virtual bool IsDocument()
        {
            return false;
        }

        public Extension()
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float
                | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft
                | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight
                | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop
                | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom;
            if (this.IsDocument())
                this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.CloseButton = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Docker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
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

        protected override string GetPersistString()
        {
            return string.Format("Type={0};TabText={1}", GetType().Name, TabText);
        }

        public virtual bool LoadFromPersistString(PersistStringParser parser)
        {
            this.TabText = parser["TabText"];
            return true;
        }
    }
}
