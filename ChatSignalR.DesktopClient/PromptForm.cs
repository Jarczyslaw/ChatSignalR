using System;
using System.Windows.Forms;

namespace ChatSignalR.DesktopClient
{
    public partial class PromptForm : Form
    {
        public PromptForm()
        {
            InitializeComponent();
        }

        public static string ShowForm(string caption, string text, string value = null)
        {
            var form = new PromptForm
            {
                Text = caption,
            };
            form.lblText.Text = text;
            form.tbValue.Text = value;
            form.StartPosition = FormStartPosition.CenterParent;
            return form.ShowDialog() == DialogResult.OK ? form.tbValue.Text : null;
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void PromptForm_Shown(object sender, EventArgs e)
        {
            tbValue.Focus();
            tbValue.SelectionStart = tbValue.TextLength;
            tbValue.ScrollToCaret();
        }
    }
}