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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            HideForm();
            Application.ApplicationExit += Application_ApplicationExit;
        }
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideForm();
            }
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            
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
    }
}
