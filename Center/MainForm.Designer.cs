using System.Windows.Forms;
partial class MainForm
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

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.DockerContainer = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(419, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // DockerContainer
            // 
            this.DockerContainer.AllowDrop = true;
            this.DockerContainer.AutoSize = true;
            this.DockerContainer.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.DockerContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DockerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockerContainer.DockBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.DockerContainer.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.DockerContainer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DockerContainer.Location = new System.Drawing.Point(0, 24);
            this.DockerContainer.Name = "DockerContainer";
            this.DockerContainer.Size = new System.Drawing.Size(419, 238);
            this.DockerContainer.TabIndex = 3;

            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(419, 262);
            this.Controls.Add(this.DockerContainer);
            this.Controls.Add(this.MainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "r";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.MenuStrip MainMenu;
    public WeifenLuo.WinFormsUI.Docking.DockPanel DockerContainer;
}

