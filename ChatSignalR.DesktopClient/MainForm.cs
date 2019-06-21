using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            do
            {
                userName = Prompt.ShowDialog("Enter user name:", "ChatSignalR");
            }
            while (string.IsNullOrEmpty(userName));
            tbUserName.Text = userName;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            GetUserName();
        }
    }
}
