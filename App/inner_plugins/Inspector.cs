using Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

    [Plugins.PluginVersion(Name = "Inspector", Version = 1)]

    public class Inspector : Docker, Plugins.Plugin
    {
        private PropertyGrid propertyGrid1;

        public Inspector()
        {
            InitializeComponent();
        }

        public bool OnInit()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool OnExit()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void InitializeComponent()
        {
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(284, 367);
            this.propertyGrid1.TabIndex = 0;
            // 
            // Inspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(284, 367);
            this.Controls.Add(this.propertyGrid1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Inspector";
            this.ResumeLayout(false);

        }
    }
