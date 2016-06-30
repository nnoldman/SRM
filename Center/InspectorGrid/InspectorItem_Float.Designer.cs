namespace Core
{
    partial class InspectorItem_Float
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Title = new System.Windows.Forms.Label();
            this.Content = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoEllipsis = true;
            this.Title.Dock = System.Windows.Forms.DockStyle.Left;
            this.Title.Location = new System.Drawing.Point(0, 0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(108, 18);
            this.Title.TabIndex = 0;
            this.Title.Text = "label1";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Content
            // 
            this.Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Content.Location = new System.Drawing.Point(108, 0);
            this.Content.Name = "Content";
            this.Content.Size = new System.Drawing.Size(173, 21);
            this.Content.TabIndex = 1;
            this.Content.TextChanged += new System.EventHandler(this.Content_TextChanged);
            this.Content.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Content_KeyPress);
            // 
            // InspectorItem_Float
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Content);
            this.Controls.Add(this.Title);
            this.Name = "InspectorItem_Float";
            this.Size = new System.Drawing.Size(281, 18);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label Title;
        public System.Windows.Forms.TextBox Content;
    }
}
