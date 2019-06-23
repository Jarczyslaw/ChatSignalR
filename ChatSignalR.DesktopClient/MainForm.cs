using System;
using System.Windows.Forms;

namespace ChatSignalR.DesktopClient
{
    public partial class MainForm : Form
    {
        private IChatService chatService;

        public MainForm()
        {
            InitializeComponent();
            chatService = new ChatService();
        }

        private void GetUserName()
        {
            string userName;
            while (true)
            {
                userName = PromptForm.ShowForm("ChatSignalR", "Enter user name:", $"TestUser{new Random().Next(1, 9999)}");
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
            chatService.Connect();
        }
    }
}