using System;
using System.Windows.Forms;

namespace ChatSignalR.DesktopClient
{
    public partial class MainForm : Form
    {
        private readonly IChatService chatService;
        private string userName;

        public MainForm(IChatService chatService)
        {
            InitializeComponent();
            this.chatService = chatService;
        }

        private void GetUserName()
        {
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
            tbMessage.Focus();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            GetUserName();
            InitializeChatService();
        }

        private async void InitializeChatService()
        {
            try
            {
                await chatService.Connect();
                chatService.OnMessageReceived += AppendMessage;
                chatService.OnStatusReceived += UpdateStatus;
            }
            catch (Exception exc)
            {
                MessageBoxUtils.ShowException(exc);
            }
        }

        private void UpdateStatus(string status)
        {
            ThreadUtils.SafeInvoke(this, () => tbStatus.Text = status);
        }

        private void AppendMessage(string userName, string message)
        {
            ThreadUtils.SafeInvoke(this, () => tbDiscussion.Text += userName + ": " + message + Environment.NewLine);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbMessage.Text))
            {
                try
                {
                    chatService.Send(userName, tbMessage.Text);
                    tbMessage.Text = string.Empty;
                }
                catch (Exception exc)
                {
                    MessageBoxUtils.ShowException(exc);
                }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            tbDiscussion.Text = string.Empty;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                chatService.Disconnect();
            }
            catch(Exception exc)
            {
                MessageBoxUtils.ShowException(exc);
            }
        }
    }
}