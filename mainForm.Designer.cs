namespace WindowTool
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mainMenuStrip = new System.Windows.Forms.MenuStrip();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            notifyIcon = new System.Windows.Forms.NotifyIcon(components);
            notifyMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            clampMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            iconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            mainMenuStrip.SuspendLayout();
            notifyMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { aboutToolStripMenuItem });
            mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Size = new System.Drawing.Size(620, 24);
            mainMenuStrip.TabIndex = 0;
            mainMenuStrip.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = notifyMenuStrip;
            notifyIcon.Icon = (System.Drawing.Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Window Tool";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // notifyMenuStrip
            // 
            notifyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { settingsMenuItem, clampMenuItem, iconMenuItem, exitMenuItem });
            notifyMenuStrip.Name = "notifyMenuStrip";
            notifyMenuStrip.Size = new System.Drawing.Size(161, 92);
            // 
            // settingsMenuItem
            // 
            settingsMenuItem.Name = "settingsMenuItem";
            settingsMenuItem.Size = new System.Drawing.Size(160, 22);
            settingsMenuItem.Text = "Settings";
            settingsMenuItem.Visible = false;
            settingsMenuItem.Click += settingsMenuItem_Click;
            // 
            // clampMenuItem
            // 
            clampMenuItem.CheckOnClick = true;
            clampMenuItem.Name = "clampMenuItem";
            clampMenuItem.Size = new System.Drawing.Size(160, 22);
            clampMenuItem.Text = "Clamp to screen";
            clampMenuItem.Click += clampMenuItem_Click;
            // 
            // iconMenuItem
            // 
            iconMenuItem.CheckOnClick = true;
            iconMenuItem.Name = "iconMenuItem";
            iconMenuItem.Size = new System.Drawing.Size(160, 22);
            iconMenuItem.Text = "Alternate icon";
            iconMenuItem.Click += iconMenuItem_Click;
            // 
            // exitMenuItem
            // 
            exitMenuItem.Name = "exitMenuItem";
            exitMenuItem.Size = new System.Drawing.Size(160, 22);
            exitMenuItem.Text = "Exit";
            exitMenuItem.Click += exitMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(620, 406);
            Controls.Add(mainMenuStrip);
            Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MainMenuStrip = mainMenuStrip;
            Margin = new System.Windows.Forms.Padding(5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Text = "WindowTool";
            FormClosing += mainForm_FormClosing;
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            notifyMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clampMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iconMenuItem;
    }
}
