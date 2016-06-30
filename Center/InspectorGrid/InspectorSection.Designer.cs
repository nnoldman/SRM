namespace Core
{
    partial class InspectorSection
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
            this.TitleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleButton
            // 
            this.TitleButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleButton.Location = new System.Drawing.Point(0, 0);
            this.TitleButton.Margin = new System.Windows.Forms.Padding(0);
            this.TitleButton.Name = "TitleButton";
            this.TitleButton.Size = new System.Drawing.Size(219, 23);
            this.TitleButton.TabIndex = 0;
            this.TitleButton.Text = "button1";
            this.TitleButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TitleButton.UseVisualStyleBackColor = true;
            this.TitleButton.Click += new System.EventHandler(this.TitleButton_Click_1);
            // 
            // InspectorSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TitleButton);
            this.Name = "InspectorSection";
            this.Size = new System.Drawing.Size(219, 380);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TitleButton;
    }
}
