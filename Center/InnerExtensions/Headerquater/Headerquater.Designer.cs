namespace Core
{
    partial class Headerquater
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.portPanel = new System.Windows.Forms.Panel();
            this.inpanel = new System.Windows.Forms.Panel();
            this.outpanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.portPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 320);
            this.panel1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(200, 320);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(504, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 320);
            this.panel2.TabIndex = 1;
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(0, 0);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(200, 320);
            this.listBox2.TabIndex = 0;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // portPanel
            // 
            this.portPanel.AutoSize = true;
            this.portPanel.Controls.Add(this.inpanel);
            this.portPanel.Controls.Add(this.outpanel);
            this.portPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portPanel.Location = new System.Drawing.Point(200, 0);
            this.portPanel.Name = "portPanel";
            this.portPanel.Size = new System.Drawing.Size(304, 320);
            this.portPanel.TabIndex = 2;
            // 
            // inpanel
            // 
            this.inpanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.inpanel.Location = new System.Drawing.Point(174, 0);
            this.inpanel.Name = "inpanel";
            this.inpanel.Size = new System.Drawing.Size(130, 320);
            this.inpanel.TabIndex = 1;
            // 
            // outpanel
            // 
            this.outpanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.outpanel.Location = new System.Drawing.Point(0, 0);
            this.outpanel.Name = "outpanel";
            this.outpanel.Size = new System.Drawing.Size(130, 320);
            this.outpanel.TabIndex = 0;
            // 
            // Headerquater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 320);
            this.Controls.Add(this.portPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MinimumSize = new System.Drawing.Size(720, 358);
            this.Name = "Headerquater";
            this.Text = "Headerquater";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.portPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel portPanel;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Panel inpanel;
        private System.Windows.Forms.Panel outpanel;
    }
}