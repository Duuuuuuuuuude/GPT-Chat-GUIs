﻿using System.Windows.Forms;

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
            txtLogitBias = new TextBox();
            lblLogitBias = new Label();
            txtStop = new TextBox();
            lblStop = new Label();
            txtPresencePenalty = new TextBox();
            lblPresencePenalty = new Label();
            txtFrequencyPenalty = new TextBox();
            lblFrequencyPenalty = new Label();
            txtN = new TextBox();
            lblN = new Label();
            txtTopP = new TextBox();
            lblTopP = new Label();
            txtTemperature = new TextBox();
            lblTemperature = new Label();
            txtTokenLimit = new TextBox();
            lblTokenLimit = new Label();
            txtMaxResponseTokens = new TextBox();
            lblMaxResponseTokens = new Label();
            txtModelId = new TextBox();
            lblModelId = new Label();
            txtUsername = new TextBox();
            lblUserName = new Label();
            txtOpenAiKey = new TextBox();
            lblOpenAiKey = new Label();
            txtOpenAiOrganization = new TextBox();
            lblOpenAiOrganization = new Label();
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
            splitContainerContentAndSettings.Panel1.Controls.Add(txtLogitBias);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblLogitBias);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtStop);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblStop);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtPresencePenalty);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblPresencePenalty);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtFrequencyPenalty);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblFrequencyPenalty);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtN);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblN);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtTopP);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblTopP);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtTemperature);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblTemperature);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtTokenLimit);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblTokenLimit);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtMaxResponseTokens);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblMaxResponseTokens);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtModelId);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblModelId);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtUsername);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblUserName);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtOpenAiKey);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblOpenAiKey);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtOpenAiOrganization);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblOpenAiOrganization);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblTokenCostFullConversation);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblFinishReason);
            splitContainerContentAndSettings.Panel1.Controls.Add(BtnCloseSelectedTab);
            splitContainerContentAndSettings.Panel1.Controls.Add(BtnNewTab);
            // 
            // splitContainerContentAndSettings.Panel2
            // 
            splitContainerContentAndSettings.Panel2.Controls.Add(tabCtrlChats);
            splitContainerContentAndSettings.Size = new Size(1184, 776);
            splitContainerContentAndSettings.SplitterDistance = 296;
            splitContainerContentAndSettings.TabIndex = 0;
            // 
            // txtLogitBias
            // 
            txtLogitBias.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtLogitBias.Location = new Point(13, 715);
            txtLogitBias.Name = "txtLogitBias";
            txtLogitBias.Size = new Size(275, 27);
            txtLogitBias.TabIndex = 29;
            txtLogitBias.TextChanged += txtLogitBias_TextChanged;
            // 
            // lblLogitBias
            // 
            lblLogitBias.AutoSize = true;
            lblLogitBias.Location = new Point(13, 692);
            lblLogitBias.Name = "lblLogitBias";
            lblLogitBias.Size = new Size(74, 20);
            lblLogitBias.TabIndex = 28;
            lblLogitBias.Text = "Logit Bias";
            // 
            // txtStop
            // 
            txtStop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtStop.Location = new Point(12, 662);
            txtStop.Name = "txtStop";
            txtStop.Size = new Size(276, 27);
            txtStop.TabIndex = 27;
            txtStop.TextChanged += txtStop_TextChanged;
            // 
            // lblStop
            // 
            lblStop.AutoSize = true;
            lblStop.Location = new Point(13, 640);
            lblStop.Name = "lblStop";
            lblStop.Size = new Size(40, 20);
            lblStop.TabIndex = 26;
            lblStop.Text = "Stop";
            // 
            // txtPresencePenalty
            // 
            txtPresencePenalty.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPresencePenalty.Location = new Point(12, 609);
            txtPresencePenalty.Name = "txtPresencePenalty";
            txtPresencePenalty.Size = new Size(276, 27);
            txtPresencePenalty.TabIndex = 25;
            txtPresencePenalty.TextChanged += txtPresencePenalty_TextChanged;
            // 
            // lblPresencePenalty
            // 
            lblPresencePenalty.AutoSize = true;
            lblPresencePenalty.Location = new Point(13, 587);
            lblPresencePenalty.Name = "lblPresencePenalty";
            lblPresencePenalty.Size = new Size(118, 20);
            lblPresencePenalty.TabIndex = 24;
            lblPresencePenalty.Text = "Presence Penalty";
            // 
            // txtFrequencyPenalty
            // 
            txtFrequencyPenalty.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFrequencyPenalty.Location = new Point(12, 556);
            txtFrequencyPenalty.Name = "txtFrequencyPenalty";
            txtFrequencyPenalty.Size = new Size(276, 27);
            txtFrequencyPenalty.TabIndex = 23;
            txtFrequencyPenalty.TextChanged += txtFrequencyPenalty_TextChanged;
            // 
            // lblFrequencyPenalty
            // 
            lblFrequencyPenalty.AutoSize = true;
            lblFrequencyPenalty.Location = new Point(13, 534);
            lblFrequencyPenalty.Name = "lblFrequencyPenalty";
            lblFrequencyPenalty.Size = new Size(127, 20);
            lblFrequencyPenalty.TabIndex = 22;
            lblFrequencyPenalty.Text = "Frequency Penalty";
            // 
            // txtN
            // 
            txtN.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtN.Location = new Point(12, 503);
            txtN.Name = "txtN";
            txtN.Size = new Size(276, 27);
            txtN.TabIndex = 21;
            txtN.TextChanged += txtN_TextChanged;
            // 
            // lblN
            // 
            lblN.AutoSize = true;
            lblN.Location = new Point(13, 481);
            lblN.Name = "lblN";
            lblN.Size = new Size(20, 20);
            lblN.TabIndex = 20;
            lblN.Text = "N";
            // 
            // txtTopP
            // 
            txtTopP.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTopP.Location = new Point(12, 450);
            txtTopP.Name = "txtTopP";
            txtTopP.Size = new Size(276, 27);
            txtTopP.TabIndex = 19;
            txtTopP.TextChanged += txtTopP_TextChanged;
            // 
            // lblTopP
            // 
            lblTopP.AutoSize = true;
            lblTopP.Location = new Point(13, 428);
            lblTopP.Name = "lblTopP";
            lblTopP.Size = new Size(46, 20);
            lblTopP.TabIndex = 18;
            lblTopP.Text = "Top P";
            // 
            // txtTemperature
            // 
            txtTemperature.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTemperature.Location = new Point(12, 397);
            txtTemperature.Name = "txtTemperature";
            txtTemperature.Size = new Size(276, 27);
            txtTemperature.TabIndex = 17;
            txtTemperature.TextChanged += txtTemperature_TextChanged;
            // 
            // lblTemperature
            // 
            lblTemperature.AutoSize = true;
            lblTemperature.Location = new Point(12, 374);
            lblTemperature.Name = "lblTemperature";
            lblTemperature.Size = new Size(93, 20);
            lblTemperature.TabIndex = 16;
            lblTemperature.Text = "Temperature";
            // 
            // txtTokenLimit
            // 
            txtTokenLimit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTokenLimit.Location = new Point(12, 344);
            txtTokenLimit.Name = "txtTokenLimit";
            txtTokenLimit.Size = new Size(276, 27);
            txtTokenLimit.TabIndex = 15;
            txtTokenLimit.TextChanged += txtTokenLimit_TextChanged;
            // 
            // lblTokenLimit
            // 
            lblTokenLimit.AutoSize = true;
            lblTokenLimit.Location = new Point(12, 321);
            lblTokenLimit.Name = "lblTokenLimit";
            lblTokenLimit.Size = new Size(85, 20);
            lblTokenLimit.TabIndex = 14;
            lblTokenLimit.Text = "Token Limit";
            // 
            // txtMaxResponseTokens
            // 
            txtMaxResponseTokens.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMaxResponseTokens.Location = new Point(12, 291);
            txtMaxResponseTokens.Name = "txtMaxResponseTokens";
            txtMaxResponseTokens.Size = new Size(276, 27);
            txtMaxResponseTokens.TabIndex = 13;
            txtMaxResponseTokens.TextChanged += txtMaxResponseTokens_TextChanged;
            // 
            // lblMaxResponseTokens
            // 
            lblMaxResponseTokens.AutoSize = true;
            lblMaxResponseTokens.Location = new Point(12, 268);
            lblMaxResponseTokens.Name = "lblMaxResponseTokens";
            lblMaxResponseTokens.Size = new Size(153, 20);
            lblMaxResponseTokens.TabIndex = 12;
            lblMaxResponseTokens.Text = "Max Response Tokens";
            // 
            // txtModelId
            // 
            txtModelId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtModelId.Location = new Point(12, 238);
            txtModelId.Name = "txtModelId";
            txtModelId.Size = new Size(276, 27);
            txtModelId.TabIndex = 11;
            txtModelId.TextChanged += txtModelId_TextChanged;
            // 
            // lblModelId
            // 
            lblModelId.AutoSize = true;
            lblModelId.Location = new Point(12, 215);
            lblModelId.Name = "lblModelId";
            lblModelId.Size = new Size(69, 20);
            lblModelId.TabIndex = 10;
            lblModelId.Text = "Model Id";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Location = new Point(12, 185);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(276, 27);
            txtUsername.TabIndex = 9;
            txtUsername.TextChanged += txtUsername_TextChanged;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(12, 162);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(75, 20);
            lblUserName.TabIndex = 8;
            lblUserName.Text = "Username";
            // 
            // txtOpenAiKey
            // 
            txtOpenAiKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtOpenAiKey.Location = new Point(12, 132);
            txtOpenAiKey.Name = "txtOpenAiKey";
            txtOpenAiKey.Size = new Size(276, 27);
            txtOpenAiKey.TabIndex = 7;
            txtOpenAiKey.TextChanged += txtOpenAiKey_TextChanged;
            // 
            // lblOpenAiKey
            // 
            lblOpenAiKey.AutoSize = true;
            lblOpenAiKey.Location = new Point(12, 109);
            lblOpenAiKey.Name = "lblOpenAiKey";
            lblOpenAiKey.Size = new Size(87, 20);
            lblOpenAiKey.TabIndex = 6;
            lblOpenAiKey.Text = "OpenAi Key";
            // 
            // txtOpenAiOrganization
            // 
            txtOpenAiOrganization.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtOpenAiOrganization.Location = new Point(12, 79);
            txtOpenAiOrganization.Name = "txtOpenAiOrganization";
            txtOpenAiOrganization.Size = new Size(276, 27);
            txtOpenAiOrganization.TabIndex = 5;
            txtOpenAiOrganization.TextChanged += txtOpenAiOrganization_TextChanged;
            // 
            // lblOpenAiOrganization
            // 
            lblOpenAiOrganization.AutoSize = true;
            lblOpenAiOrganization.Location = new Point(12, 56);
            lblOpenAiOrganization.Name = "lblOpenAiOrganization";
            lblOpenAiOrganization.Size = new Size(149, 20);
            lblOpenAiOrganization.TabIndex = 4;
            lblOpenAiOrganization.Text = "OpenAI Organization";
            // 
            // lblTokenCostFullConversation
            // 
            lblTokenCostFullConversation.AutoSize = true;
            lblTokenCostFullConversation.Location = new Point(0, 32);
            lblTokenCostFullConversation.Name = "lblTokenCostFullConversation";
            lblTokenCostFullConversation.Size = new Size(160, 20);
            lblTokenCostFullConversation.TabIndex = 3;
            lblTokenCostFullConversation.Text = "&Token Cost This Chat: 0";
            // 
            // lblFinishReason
            // 
            lblFinishReason.AutoSize = true;
            lblFinishReason.Location = new Point(12, 9);
            lblFinishReason.Name = "lblFinishReason";
            lblFinishReason.Size = new Size(105, 20);
            lblFinishReason.TabIndex = 2;
            lblFinishReason.Text = "Finish Reason: ";
            // 
            // BtnCloseSelectedTab
            // 
            BtnCloseSelectedTab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnCloseSelectedTab.Location = new Point(165, 47);
            BtnCloseSelectedTab.Name = "BtnCloseSelectedTab";
            BtnCloseSelectedTab.Size = new Size(123, 29);
            BtnCloseSelectedTab.TabIndex = 1;
            BtnCloseSelectedTab.Text = "&Close Selected Tab";
            BtnCloseSelectedTab.UseVisualStyleBackColor = true;
            BtnCloseSelectedTab.Click += BtnCloseTab_Click;
            // 
            // BtnNewTab
            // 
            BtnNewTab.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnNewTab.Location = new Point(194, 12);
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
            tabCtrlChats.Size = new Size(884, 776);
            tabCtrlChats.TabIndex = 0;
            // 
            // tabPageChat1
            // 
            tabPageChat1.Controls.Add(splitContainerChat1);
            tabPageChat1.Location = new Point(4, 29);
            tabPageChat1.Name = "tabPageChat1";
            tabPageChat1.Padding = new Padding(3);
            tabPageChat1.Size = new Size(876, 743);
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
            splitContainerChat1.Size = new Size(870, 737);
            splitContainerChat1.SplitterDistance = 663;
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
            webView2Chat1.Size = new Size(870, 663);
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
            txtBoxInput.Size = new Size(761, 62);
            txtBoxInput.TabIndex = 1;
            txtBoxInput.KeyDown += TxtBoxInput_KeyDown_Async;
            // 
            // btnSendMessage
            // 
            btnSendMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnSendMessage.Location = new Point(770, 4);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(94, 62);
            btnSendMessage.TabIndex = 0;
            btnSendMessage.Text = "&Send";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click_Async;
            // 
            // ChatsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 776);
            Controls.Add(splitContainerContentAndSettings);
            Name = "ChatsForm";
            Text = "GPT Chats";
            WindowState = FormWindowState.Maximized;
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
        private TabControl tabCtrlChats;
        private TabPage tabPageChat1;
        private SplitContainer splitContainerChat1;
        private TextBox txtBoxInput;
        private Button btnSendMessage;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView2Chat1;
        private TextBox textBox10;
        private Label lblLogitBias;
        private TextBox txtLogitBias;
        private TextBox txtStop;
        private Label lblStop;
        private TextBox txtPresencePenalty;
        private Label lblPresencePenalty;
        private TextBox txtFrequencyPenalty;
        private Label lblFrequencyPenalty;
        private TextBox txtN;
        private Label lblN;
        private TextBox txtTopP;
        private Label lblTopP;
        private TextBox txtTemperature;
        private Label lblTemperature;
        private TextBox txtTokenLimit;
        private Label lblTokenLimit;
        private TextBox txtMaxResponseTokens;
        private Label lblMaxResponseTokens;
        private TextBox txtModelId;
        private Label lblModelId;
        private TextBox txtUsername;
        private Label lblUserName;
        private TextBox txtOpenAiKey;
        private Label lblOpenAiKey;
        private TextBox txtOpenAiOrganization;
        private Label lblOpenAiOrganization;
        private Label lblTokenCostFullConversation;
        private Label lblFinishReason;
        private Button BtnCloseSelectedTab;
        private Button BtnNewTab;
        private TextBox textBox11;
        private Label label11;
        private TextBox textBox12;
        private Label label12;
        private Label label13;
        private TextBox textBox14;
        private Label label14;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox6;
    }
}