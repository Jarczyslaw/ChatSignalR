using System;
using System.Windows.Forms;

namespace ChatSignalR.DesktopClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void GetUserName()
        {
            string userName;
            while (true)
            {
                userName = PromptForm.ShowForm("ChatSignalR", "Enter user name:", string.Empty);
                if (userName == null)
                {
                    Close();
                    return;
                }
                else if (userName != string.Empty)
                {
                    break;
                }
            }
            tbUserName.Text = userName;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            GetUserName();
        }
    }
}