using System;
using System.Threading.Tasks;
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

        private bool GetUserName()
        {
            while (true)
            {
                userName = PromptForm.ShowForm("ChatSignalR", "Enter user name:", $"TestUser{new Random().Next(1, 99999)}");
                if (userName == null)
                {
                    return false;
                }
                else if (userName != string.Empty)
                {
                    tbUserName.Text = userName;
                    tbMessage.Focus();
                    return true;
                }
            }
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                if (GetUserName())
                {
                    await InitializeChatService();
                }
                else
                {
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBoxUtils.ShowException(exc);
            }
        }

        private async Task InitializeChatService()
        {
            try
            {
                chatService.OnMessageReceived += AppendMessage;
                chatService.OnStatusReceived += UpdateStatus;
                await chatService.Connect(userName);
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
            ThreadUtils.SafeInvoke(this, () =>
            {
                tbDiscussion.Text += userName + ": " + message + Environment.NewLine;
                ScrollDiscussion();
            });
        }

        private void ScrollDiscussion()
        {
            tbDiscussion.SelectionStart = tbDiscussion.Text.Length;
            tbDiscussion.ScrollToCaret();
        }

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        private async Task SendMessage()
        {
            if (!string.IsNullOrEmpty(tbMessage.Text))
            {
                try
                {
                    await chatService.SendMessage(userName, tbMessage.Text);
                    tbMessage.Text = string.Empty;
                    tbMessage.Focus();
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
            catch (Exception exc)
            {
                MessageBoxUtils.ShowException(exc);
            }
        }

        private async void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await SendMessage();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}