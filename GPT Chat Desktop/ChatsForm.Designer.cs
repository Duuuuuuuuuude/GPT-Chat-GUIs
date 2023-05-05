using System.Windows.Forms;

namespace GPT_Chat_Desktop
{
    partial class ChatsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainerContentAndSettings = new SplitContainer();
            lblTokenCostFullConversation = new Label();
            lblFinishReason = new Label();
            BtnCloseSelectedTab = new Button();
            BtnNewTab = new Button();
            tabCtrlChats = new TabControl();
            tabPageChat1 = new TabPage();
            splitContainerChat1 = new SplitContainer();
            webView2Chat1 = new Microsoft.Web.WebView2.WinForms.WebView2();
            txtBoxInput = new TextBox();
            btnSendMessage = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainerContentAndSettings).BeginInit();
            splitContainerContentAndSettings.Panel1.SuspendLayout();
            splitContainerContentAndSettings.Panel2.SuspendLayout();
            splitContainerContentAndSettings.SuspendLayout();
            tabCtrlChats.SuspendLayout();
            tabPageChat1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerChat1).BeginInit();
            splitContainerChat1.Panel1.SuspendLayout();
            splitContainerChat1.Panel2.SuspendLayout();
            splitContainerChat1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView2Chat1).BeginInit();
            SuspendLayout();
            // 
            // splitContainerContentAndSettings
            // 
            splitContainerContentAndSettings.Dock = DockStyle.Fill;
            splitContainerContentAndSettings.Location = new Point(0, 0);
            splitContainerContentAndSettings.Name = "splitContainerContentAndSettings";
            // 
            // splitContainerContentAndSettings.Panel1
            // 
            splitContainerContentAndSettings.Panel1.Controls.Add(lblTokenCostFullConversation);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblFinishReason);
            splitContainerContentAndSettings.Panel1.Controls.Add(BtnCloseSelectedTab);
            splitContainerContentAndSettings.Panel1.Controls.Add(BtnNewTab);
            // 
            // splitContainerContentAndSettings.Panel2
            // 
            splitContainerContentAndSettings.Panel2.Controls.Add(tabCtrlChats);
            splitContainerContentAndSettings.Size = new Size(800, 450);
            splitContainerContentAndSettings.SplitterDistance = 200;
            splitContainerContentAndSettings.TabIndex = 0;
            // 
            // lblTokenCostFullConversation
            // 
            lblTokenCostFullConversation.AutoSize = true;
            lblTokenCostFullConversation.Location = new Point(12, 99);
            lblTokenCostFullConversation.Name = "lblTokenCostFullConversation";
            lblTokenCostFullConversation.Size = new Size(160, 20);
            lblTokenCostFullConversation.TabIndex = 3;
            lblTokenCostFullConversation.Text = "&Token Cost This Chat: 0";
            // 
            // lblFinishReason
            // 
            lblFinishReason.AutoSize = true;
            lblFinishReason.Location = new Point(12, 79);
            lblFinishReason.Name = "lblFinishReason";
            lblFinishReason.Size = new Size(105, 20);
            lblFinishReason.TabIndex = 2;
            lblFinishReason.Text = "Finish Reason: ";
            // 
            // BtnCloseSelectedTab
            // 
            BtnCloseSelectedTab.Location = new Point(12, 47);
            BtnCloseSelectedTab.Name = "BtnCloseSelectedTab";
            BtnCloseSelectedTab.Size = new Size(123, 29);
            BtnCloseSelectedTab.TabIndex = 1;
            BtnCloseSelectedTab.Text = "&Close Selected Tab";
            BtnCloseSelectedTab.UseVisualStyleBackColor = true;
            BtnCloseSelectedTab.Click += BtnCloseTab_Click;
            // 
            // BtnNewTab
            // 
            BtnNewTab.Location = new Point(12, 12);
            BtnNewTab.Name = "BtnNewTab";
            BtnNewTab.Size = new Size(94, 29);
            BtnNewTab.TabIndex = 0;
            BtnNewTab.Text = "&New Tab";
            BtnNewTab.UseVisualStyleBackColor = true;
            BtnNewTab.Click += BtnNewTab_Click;
            // 
            // tabCtrlChats
            // 
            tabCtrlChats.Controls.Add(tabPageChat1);
            tabCtrlChats.Dock = DockStyle.Fill;
            tabCtrlChats.Location = new Point(0, 0);
            tabCtrlChats.Name = "tabCtrlChats";
            tabCtrlChats.SelectedIndex = 0;
            tabCtrlChats.Size = new Size(596, 450);
            tabCtrlChats.TabIndex = 0;
            // 
            // tabPageChat1
            // 
            tabPageChat1.Controls.Add(splitContainerChat1);
            tabPageChat1.Location = new Point(4, 29);
            tabPageChat1.Name = "tabPageChat1";
            tabPageChat1.Padding = new Padding(3);
            tabPageChat1.Size = new Size(588, 417);
            tabPageChat1.TabIndex = 0;
            tabPageChat1.Text = "Chat 1";
            tabPageChat1.UseVisualStyleBackColor = true;
            // 
            // splitContainerChat1
            // 
            splitContainerChat1.Dock = DockStyle.Fill;
            splitContainerChat1.Location = new Point(3, 3);
            splitContainerChat1.Name = "splitContainerChat1";
            splitContainerChat1.Orientation = Orientation.Horizontal;
            // 
            // splitContainerChat1.Panel1
            // 
            splitContainerChat1.Panel1.Controls.Add(webView2Chat1);
            // 
            // splitContainerChat1.Panel2
            // 
            splitContainerChat1.Panel2.Controls.Add(txtBoxInput);
            splitContainerChat1.Panel2.Controls.Add(btnSendMessage);
            splitContainerChat1.Size = new Size(582, 411);
            splitContainerChat1.SplitterDistance = 370;
            splitContainerChat1.TabIndex = 0;
            // 
            // webView2Chat1
            // 
            webView2Chat1.AllowExternalDrop = true;
            webView2Chat1.CreationProperties = null;
            webView2Chat1.DefaultBackgroundColor = Color.White;
            webView2Chat1.Dock = DockStyle.Fill;
            webView2Chat1.Location = new Point(0, 0);
            webView2Chat1.Name = "webView2Chat1";
            webView2Chat1.Size = new Size(582, 370);
            webView2Chat1.TabIndex = 0;
            webView2Chat1.ZoomFactor = 1D;
            // 
            // txtBoxInput
            // 
            txtBoxInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxInput.Location = new Point(3, 5);
            txtBoxInput.MaxLength = 0;
            txtBoxInput.Multiline = true;
            txtBoxInput.Name = "txtBoxInput";
            txtBoxInput.Size = new Size(473, 29);
            txtBoxInput.TabIndex = 1;
            txtBoxInput.KeyDown += TxtBoxInput_KeyDown_Async;
            // 
            // btnSendMessage
            // 
            btnSendMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnSendMessage.Location = new Point(482, 4);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(94, 29);
            btnSendMessage.TabIndex = 0;
            btnSendMessage.Text = "&Send";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click_Async;
            // 
            // ChatsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainerContentAndSettings);
            Name = "ChatsForm";
            Text = "GPT Chats";
            Load += ChatsForm_Load;
            splitContainerContentAndSettings.Panel1.ResumeLayout(false);
            splitContainerContentAndSettings.Panel1.PerformLayout();
            splitContainerContentAndSettings.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerContentAndSettings).EndInit();
            splitContainerContentAndSettings.ResumeLayout(false);
            tabCtrlChats.ResumeLayout(false);
            tabPageChat1.ResumeLayout(false);
            splitContainerChat1.Panel1.ResumeLayout(false);
            splitContainerChat1.Panel2.ResumeLayout(false);
            splitContainerChat1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerChat1).EndInit();
            splitContainerChat1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView2Chat1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainerContentAndSettings;
        private Button BtnCloseSelectedTab;
        private Button BtnNewTab;
        private TabControl tabCtrlChats;
        private TabPage tabPageChat1;
        private SplitContainer splitContainerChat1;
        private TextBox txtBoxInput;
        private Button btnSendMessage;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2Chat1;
        private Label lblFinishReason;
        private Label lblTokenCostFullConversation;
    }
}