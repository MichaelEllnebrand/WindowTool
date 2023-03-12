using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowTool
{
    public partial class MainForm : Form
    {
        HotkeyHandler hotkeyHandler;

        public MainForm()
        {
            InitializeComponent();

            hotkeyHandler = new HotkeyHandler();
            hotkeyHandler.Start();

            Application.ApplicationExit += Application_ApplicationExit;

            iconMenuItem.Checked = Properties.Settings.Default.AltIcon;
            bool isClamped = Properties.Settings.Default.ClampedToScreen;
            handleSetClampToScreen(isClamped);

            HideForm();
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            hotkeyHandler.Stop();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideForm();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            clampMenuItem.Checked = !clampMenuItem.Checked;
            clampMenuItem_Click(this, EventArgs.Empty);
        }

        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void ShowForm()
        {
            this.Show();
            if (this.WindowState == FormWindowState.Minimized) this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void HideForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void clampMenuItem_Click(object sender, EventArgs e)
        {
            handleSetClampToScreen(clampMenuItem.Checked);
        }
        private void iconMenuItem_Click(object sender, EventArgs e)
        {
            updateIcon();
        }

        private void handleSetClampToScreen(bool isClamped)
        {
            Properties.Settings.Default.ClampedToScreen = isClamped;
            Properties.Settings.Default.Save();
            clampMenuItem.Checked = isClamped;
            hotkeyHandler.SetClampToScreen(isClamped);
            updateIcon();

            /*
            if (isClamped)
            {
                notifyIcon.Icon = iconMenuItem.Checked ? Properties.Resources.ClampTrue : Properties.Resources.AltClampTrue;
                //notifyIcon.Icon = WindowTool.Properties.Resources.AltClampTrue;
            }
            else
            {
                notifyIcon.Icon = iconMenuItem.Checked ? Properties.Resources.ClampFalse : Properties.Resources.AltClampFalse;
                //notifyIcon.Icon = WindowTool.Properties.Resources.AltClampFalse;
            }
            */
        }

        private void updateIcon()
        {
            Properties.Settings.Default.AltIcon = iconMenuItem.Checked;
            Properties.Settings.Default.Save();

            if (clampMenuItem.Checked)
            {
                notifyIcon.Icon = iconMenuItem.Checked ? Properties.Resources.AltClampTrue : Properties.Resources.ClampTrue;
            }
            else
            {
                notifyIcon.Icon = iconMenuItem.Checked ? Properties.Resources.AltClampFalse : Properties.Resources.ClampFalse;
            }
        }

    }
}
