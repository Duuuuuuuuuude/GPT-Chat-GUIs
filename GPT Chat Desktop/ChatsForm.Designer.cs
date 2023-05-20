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
            txtBoxFirstSystemMessage = new TextBox();
            lblFirstSystemMessage = new Label();
            btnTable = new Button();
            JavaMethod = new Button();
            chkShowKey = new CheckBox();
            chkShowOrganization = new CheckBox();
            txtBoxLogitBias = new TextBox();
            lblLogitBias = new Label();
            txtBoxStop = new TextBox();
            lblStop = new Label();
            txtBoxPresencePenalty = new TextBox();
            lblPresencePenalty = new Label();
            txtBoxFrequencyPenalty = new TextBox();
            lblFrequencyPenalty = new Label();
            txtBoxN = new TextBox();
            lblN = new Label();
            txtBoxTopP = new TextBox();
            lblTopP = new Label();
            txtBoxTemperature = new TextBox();
            lblTemperature = new Label();
            txtBoxTokenLimit = new TextBox();
            lblTokenLimit = new Label();
            txtBoxMaxResponseTokens = new TextBox();
            lblMaxResponseTokens = new Label();
            txtBoxModelId = new TextBox();
            lblModelId = new Label();
            txtBoxUsername = new TextBox();
            lblUserName = new Label();
            txtBoxOpenAiKey = new TextBox();
            lblOpenAiKey = new Label();
            txtBoxOpenAiOrganization = new TextBox();
            lblOpenAiOrganization = new Label();
            lblTokenCostFullConversation = new Label();
            lblFinishReason = new Label();
            BtnCloseSelectedTab = new Button();
            BtnNewTab = new Button();
            tabCtrlChats = new TabControl();
            menuStrip1 = new MenuStrip();
            ((System.ComponentModel.ISupportInitialize)splitContainerContentAndSettings).BeginInit();
            splitContainerContentAndSettings.Panel1.SuspendLayout();
            splitContainerContentAndSettings.Panel2.SuspendLayout();
            splitContainerContentAndSettings.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerContentAndSettings
            // 
            splitContainerContentAndSettings.Dock = DockStyle.Fill;
            splitContainerContentAndSettings.Location = new Point(0, 24);
            splitContainerContentAndSettings.Name = "splitContainerContentAndSettings";
            // 
            // splitContainerContentAndSettings.Panel1
            // 
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxFirstSystemMessage);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblFirstSystemMessage);
            splitContainerContentAndSettings.Panel1.Controls.Add(btnTable);
            splitContainerContentAndSettings.Panel1.Controls.Add(JavaMethod);
            splitContainerContentAndSettings.Panel1.Controls.Add(chkShowKey);
            splitContainerContentAndSettings.Panel1.Controls.Add(chkShowOrganization);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxLogitBias);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblLogitBias);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxStop);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblStop);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxPresencePenalty);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblPresencePenalty);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxFrequencyPenalty);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblFrequencyPenalty);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxN);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblN);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxTopP);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblTopP);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxTemperature);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblTemperature);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxTokenLimit);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblTokenLimit);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxMaxResponseTokens);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblMaxResponseTokens);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxModelId);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblModelId);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxUsername);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblUserName);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxOpenAiKey);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblOpenAiKey);
            splitContainerContentAndSettings.Panel1.Controls.Add(txtBoxOpenAiOrganization);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblOpenAiOrganization);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblTokenCostFullConversation);
            splitContainerContentAndSettings.Panel1.Controls.Add(lblFinishReason);
            splitContainerContentAndSettings.Panel1.Controls.Add(BtnCloseSelectedTab);
            splitContainerContentAndSettings.Panel1.Controls.Add(BtnNewTab);
            // 
            // splitContainerContentAndSettings.Panel2
            // 
            splitContainerContentAndSettings.Panel2.Controls.Add(tabCtrlChats);
            splitContainerContentAndSettings.Size = new Size(1234, 944);
            splitContainerContentAndSettings.SplitterDistance = 307;
            splitContainerContentAndSettings.TabIndex = 0;
            // 
            // txtBoxFirstSystemMessage
            // 
            txtBoxFirstSystemMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxFirstSystemMessage.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxFirstSystemMessage.Location = new Point(13, 817);
            txtBoxFirstSystemMessage.Name = "txtBoxFirstSystemMessage";
            txtBoxFirstSystemMessage.Size = new Size(286, 27);
            txtBoxFirstSystemMessage.TabIndex = 35;
            txtBoxFirstSystemMessage.TextChanged += txtBoxFirstSystemMessage_TextChanged;
            // 
            // lblFirstSystemMessage
            // 
            lblFirstSystemMessage.AutoSize = true;
            lblFirstSystemMessage.ForeColor = Color.White;
            lblFirstSystemMessage.Location = new Point(13, 794);
            lblFirstSystemMessage.Name = "lblFirstSystemMessage";
            lblFirstSystemMessage.Size = new Size(149, 20);
            lblFirstSystemMessage.TabIndex = 34;
            lblFirstSystemMessage.Text = "First System Message";
            // 
            // btnTable
            // 
            btnTable.Location = new Point(18, 897);
            btnTable.Name = "btnTable";
            btnTable.Size = new Size(94, 29);
            btnTable.TabIndex = 33;
            btnTable.Text = "&Table";
            btnTable.UseVisualStyleBackColor = true;
            btnTable.Click += btnTable_Click;
            // 
            // JavaMethod
            // 
            JavaMethod.Location = new Point(18, 862);
            JavaMethod.Name = "JavaMethod";
            JavaMethod.Size = new Size(117, 29);
            JavaMethod.TabIndex = 32;
            JavaMethod.Text = "&Java method";
            JavaMethod.UseVisualStyleBackColor = true;
            JavaMethod.Click += JavaMethod_ClickAsync;
            // 
            // chkShowKey
            // 
            chkShowKey.AutoSize = true;
            chkShowKey.ForeColor = Color.White;
            chkShowKey.Location = new Point(105, 154);
            chkShowKey.Name = "chkShowKey";
            chkShowKey.Size = new Size(95, 24);
            chkShowKey.TabIndex = 31;
            chkShowKey.Text = "Show Key";
            chkShowKey.UseVisualStyleBackColor = true;
            chkShowKey.CheckedChanged += chkShowKey_CheckedChanged;
            // 
            // chkShowOrganization
            // 
            chkShowOrganization.AutoSize = true;
            chkShowOrganization.ForeColor = Color.White;
            chkShowOrganization.Location = new Point(165, 101);
            chkShowOrganization.Name = "chkShowOrganization";
            chkShowOrganization.Size = new Size(157, 24);
            chkShowOrganization.TabIndex = 30;
            chkShowOrganization.Text = "Show Organization";
            chkShowOrganization.UseVisualStyleBackColor = true;
            chkShowOrganization.CheckedChanged += chkShowOrganization_CheckedChanged;
            // 
            // txtBoxLogitBias
            // 
            txtBoxLogitBias.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxLogitBias.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxLogitBias.Location = new Point(13, 761);
            txtBoxLogitBias.Name = "txtBoxLogitBias";
            txtBoxLogitBias.Size = new Size(286, 27);
            txtBoxLogitBias.TabIndex = 29;
            txtBoxLogitBias.TextChanged += TxtBoxLogitBiasTextChanged;
            // 
            // lblLogitBias
            // 
            lblLogitBias.AutoSize = true;
            lblLogitBias.ForeColor = Color.White;
            lblLogitBias.Location = new Point(13, 738);
            lblLogitBias.Name = "lblLogitBias";
            lblLogitBias.Size = new Size(74, 20);
            lblLogitBias.TabIndex = 28;
            lblLogitBias.Text = "Logit Bias";
            // 
            // txtBoxStop
            // 
            txtBoxStop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxStop.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxStop.Location = new Point(12, 708);
            txtBoxStop.Name = "txtBoxStop";
            txtBoxStop.Size = new Size(287, 27);
            txtBoxStop.TabIndex = 27;
            txtBoxStop.TextChanged += TxtBoxStopTextChanged;
            // 
            // lblStop
            // 
            lblStop.AutoSize = true;
            lblStop.ForeColor = Color.White;
            lblStop.Location = new Point(13, 686);
            lblStop.Name = "lblStop";
            lblStop.Size = new Size(40, 20);
            lblStop.TabIndex = 26;
            lblStop.Text = "Stop";
            // 
            // txtBoxPresencePenalty
            // 
            txtBoxPresencePenalty.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxPresencePenalty.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxPresencePenalty.Location = new Point(12, 655);
            txtBoxPresencePenalty.Name = "txtBoxPresencePenalty";
            txtBoxPresencePenalty.Size = new Size(287, 27);
            txtBoxPresencePenalty.TabIndex = 25;
            txtBoxPresencePenalty.TextChanged += TxtBoxPresencePenaltyTextChanged;
            // 
            // lblPresencePenalty
            // 
            lblPresencePenalty.AutoSize = true;
            lblPresencePenalty.ForeColor = Color.White;
            lblPresencePenalty.Location = new Point(13, 632);
            lblPresencePenalty.Name = "lblPresencePenalty";
            lblPresencePenalty.Size = new Size(118, 20);
            lblPresencePenalty.TabIndex = 24;
            lblPresencePenalty.Text = "Presence Penalty";
            // 
            // txtBoxFrequencyPenalty
            // 
            txtBoxFrequencyPenalty.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxFrequencyPenalty.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxFrequencyPenalty.Location = new Point(12, 602);
            txtBoxFrequencyPenalty.Name = "txtBoxFrequencyPenalty";
            txtBoxFrequencyPenalty.Size = new Size(287, 27);
            txtBoxFrequencyPenalty.TabIndex = 23;
            txtBoxFrequencyPenalty.TextChanged += TxtBoxFrequencyPenaltyTextChanged;
            // 
            // lblFrequencyPenalty
            // 
            lblFrequencyPenalty.AutoSize = true;
            lblFrequencyPenalty.ForeColor = Color.White;
            lblFrequencyPenalty.Location = new Point(13, 580);
            lblFrequencyPenalty.Name = "lblFrequencyPenalty";
            lblFrequencyPenalty.Size = new Size(127, 20);
            lblFrequencyPenalty.TabIndex = 22;
            lblFrequencyPenalty.Text = "Frequency Penalty";
            // 
            // txtBoxN
            // 
            txtBoxN.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxN.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxN.Location = new Point(12, 549);
            txtBoxN.Name = "txtBoxN";
            txtBoxN.Size = new Size(287, 27);
            txtBoxN.TabIndex = 21;
            txtBoxN.TextChanged += TxtBoxNTextChanged;
            // 
            // lblN
            // 
            lblN.AutoSize = true;
            lblN.ForeColor = Color.White;
            lblN.Location = new Point(13, 527);
            lblN.Name = "lblN";
            lblN.Size = new Size(20, 20);
            lblN.TabIndex = 20;
            lblN.Text = "N";
            // 
            // txtBoxTopP
            // 
            txtBoxTopP.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxTopP.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxTopP.Location = new Point(12, 496);
            txtBoxTopP.Name = "txtBoxTopP";
            txtBoxTopP.Size = new Size(287, 27);
            txtBoxTopP.TabIndex = 19;
            txtBoxTopP.TextChanged += TxtBoxTopPTextChanged;
            // 
            // lblTopP
            // 
            lblTopP.AutoSize = true;
            lblTopP.ForeColor = Color.White;
            lblTopP.Location = new Point(13, 474);
            lblTopP.Name = "lblTopP";
            lblTopP.Size = new Size(46, 20);
            lblTopP.TabIndex = 18;
            lblTopP.Text = "Top P";
            // 
            // txtBoxTemperature
            // 
            txtBoxTemperature.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxTemperature.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxTemperature.Location = new Point(12, 443);
            txtBoxTemperature.Name = "txtBoxTemperature";
            txtBoxTemperature.Size = new Size(287, 27);
            txtBoxTemperature.TabIndex = 17;
            txtBoxTemperature.TextChanged += TxtBoxTemperatureTextChanged;
            // 
            // lblTemperature
            // 
            lblTemperature.AutoSize = true;
            lblTemperature.ForeColor = Color.White;
            lblTemperature.Location = new Point(12, 420);
            lblTemperature.Name = "lblTemperature";
            lblTemperature.Size = new Size(93, 20);
            lblTemperature.TabIndex = 16;
            lblTemperature.Text = "Temperature";
            // 
            // txtBoxTokenLimit
            // 
            txtBoxTokenLimit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxTokenLimit.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxTokenLimit.Location = new Point(12, 390);
            txtBoxTokenLimit.Name = "txtBoxTokenLimit";
            txtBoxTokenLimit.Size = new Size(287, 27);
            txtBoxTokenLimit.TabIndex = 15;
            txtBoxTokenLimit.TextChanged += TxtBoxTokenLimitTextChanged;
            // 
            // lblTokenLimit
            // 
            lblTokenLimit.AutoSize = true;
            lblTokenLimit.ForeColor = Color.White;
            lblTokenLimit.Location = new Point(12, 367);
            lblTokenLimit.Name = "lblTokenLimit";
            lblTokenLimit.Size = new Size(85, 20);
            lblTokenLimit.TabIndex = 14;
            lblTokenLimit.Text = "Token Limit";
            // 
            // txtBoxMaxResponseTokens
            // 
            txtBoxMaxResponseTokens.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxMaxResponseTokens.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxMaxResponseTokens.Location = new Point(12, 337);
            txtBoxMaxResponseTokens.Name = "txtBoxMaxResponseTokens";
            txtBoxMaxResponseTokens.Size = new Size(287, 27);
            txtBoxMaxResponseTokens.TabIndex = 13;
            txtBoxMaxResponseTokens.TextChanged += TxtBoxMaxResponseTokensTextChanged;
            // 
            // lblMaxResponseTokens
            // 
            lblMaxResponseTokens.AutoSize = true;
            lblMaxResponseTokens.ForeColor = Color.White;
            lblMaxResponseTokens.Location = new Point(12, 314);
            lblMaxResponseTokens.Name = "lblMaxResponseTokens";
            lblMaxResponseTokens.Size = new Size(153, 20);
            lblMaxResponseTokens.TabIndex = 12;
            lblMaxResponseTokens.Text = "Max Response Tokens";
            // 
            // txtBoxModelId
            // 
            txtBoxModelId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxModelId.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxModelId.Location = new Point(12, 284);
            txtBoxModelId.Name = "txtBoxModelId";
            txtBoxModelId.Size = new Size(287, 27);
            txtBoxModelId.TabIndex = 11;
            txtBoxModelId.TextChanged += TxtBoxModelIdTextChanged;
            // 
            // lblModelId
            // 
            lblModelId.AutoSize = true;
            lblModelId.ForeColor = Color.White;
            lblModelId.Location = new Point(12, 261);
            lblModelId.Name = "lblModelId";
            lblModelId.Size = new Size(69, 20);
            lblModelId.TabIndex = 10;
            lblModelId.Text = "Model Id";
            // 
            // txtBoxUsername
            // 
            txtBoxUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxUsername.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxUsername.Location = new Point(12, 231);
            txtBoxUsername.Name = "txtBoxUsername";
            txtBoxUsername.Size = new Size(287, 27);
            txtBoxUsername.TabIndex = 9;
            txtBoxUsername.TextChanged += TxtBoxUsernameTextChanged;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.ForeColor = Color.White;
            lblUserName.Location = new Point(12, 208);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(75, 20);
            lblUserName.TabIndex = 8;
            lblUserName.Text = "Username";
            // 
            // txtBoxOpenAiKey
            // 
            txtBoxOpenAiKey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxOpenAiKey.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxOpenAiKey.Location = new Point(12, 178);
            txtBoxOpenAiKey.Name = "txtBoxOpenAiKey";
            txtBoxOpenAiKey.Size = new Size(287, 27);
            txtBoxOpenAiKey.TabIndex = 7;
            txtBoxOpenAiKey.UseSystemPasswordChar = true;
            txtBoxOpenAiKey.TextChanged += TxtBoxOpenAiKeyTextChanged;
            // 
            // lblOpenAiKey
            // 
            lblOpenAiKey.AutoSize = true;
            lblOpenAiKey.ForeColor = Color.White;
            lblOpenAiKey.Location = new Point(12, 155);
            lblOpenAiKey.Name = "lblOpenAiKey";
            lblOpenAiKey.Size = new Size(87, 20);
            lblOpenAiKey.TabIndex = 6;
            lblOpenAiKey.Text = "OpenAi Key";
            // 
            // txtBoxOpenAiOrganization
            // 
            txtBoxOpenAiOrganization.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBoxOpenAiOrganization.BackColor = Color.FromArgb(224, 224, 224);
            txtBoxOpenAiOrganization.Location = new Point(12, 125);
            txtBoxOpenAiOrganization.Name = "txtBoxOpenAiOrganization";
            txtBoxOpenAiOrganization.Size = new Size(287, 27);
            txtBoxOpenAiOrganization.TabIndex = 5;
            txtBoxOpenAiOrganization.UseSystemPasswordChar = true;
            txtBoxOpenAiOrganization.TextChanged += TxtBoxOpenAiOrganizationTextChanged;
            // 
            // lblOpenAiOrganization
            // 
            lblOpenAiOrganization.AutoSize = true;
            lblOpenAiOrganization.ForeColor = Color.White;
            lblOpenAiOrganization.Location = new Point(12, 102);
            lblOpenAiOrganization.Name = "lblOpenAiOrganization";
            lblOpenAiOrganization.Size = new Size(149, 20);
            lblOpenAiOrganization.TabIndex = 4;
            lblOpenAiOrganization.Text = "OpenAI Organization";
            // 
            // lblTokenCostFullConversation
            // 
            lblTokenCostFullConversation.AutoSize = true;
            lblTokenCostFullConversation.ForeColor = Color.White;
            lblTokenCostFullConversation.Location = new Point(13, 32);
            lblTokenCostFullConversation.Name = "lblTokenCostFullConversation";
            lblTokenCostFullConversation.Size = new Size(160, 20);
            lblTokenCostFullConversation.TabIndex = 3;
            lblTokenCostFullConversation.Text = "&Token Cost This Chat: 0";
            // 
            // lblFinishReason
            // 
            lblFinishReason.AutoSize = true;
            lblFinishReason.ForeColor = Color.White;
            lblFinishReason.Location = new Point(12, 9);
            lblFinishReason.Name = "lblFinishReason";
            lblFinishReason.Size = new Size(105, 20);
            lblFinishReason.TabIndex = 2;
            lblFinishReason.Text = "Finish Reason: ";
            // 
            // BtnCloseSelectedTab
            // 
            BtnCloseSelectedTab.BackColor = Color.Gray;
            BtnCloseSelectedTab.ForeColor = Color.White;
            BtnCloseSelectedTab.Location = new Point(13, 55);
            BtnCloseSelectedTab.Name = "BtnCloseSelectedTab";
            BtnCloseSelectedTab.Size = new Size(123, 29);
            BtnCloseSelectedTab.TabIndex = 1;
            BtnCloseSelectedTab.Text = "&Close Selected Tab";
            BtnCloseSelectedTab.UseVisualStyleBackColor = false;
            BtnCloseSelectedTab.Click += BtnCloseTab_Click;
            // 
            // BtnNewTab
            // 
            BtnNewTab.BackColor = Color.Gray;
            BtnNewTab.ForeColor = Color.White;
            BtnNewTab.Location = new Point(165, 55);
            BtnNewTab.Name = "BtnNewTab";
            BtnNewTab.Size = new Size(94, 29);
            BtnNewTab.TabIndex = 0;
            BtnNewTab.Text = "&New Tab";
            BtnNewTab.UseVisualStyleBackColor = false;
            BtnNewTab.Click += BtnNewTab_Click;
            // 
            // tabCtrlChats
            // 
            tabCtrlChats.Dock = DockStyle.Fill;
            tabCtrlChats.Location = new Point(0, 0);
            tabCtrlChats.Name = "tabCtrlChats";
            tabCtrlChats.SelectedIndex = 0;
            tabCtrlChats.Size = new Size(923, 944);
            tabCtrlChats.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1234, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // ChatsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(34, 34, 34);
            ClientSize = new Size(1234, 968);
            Controls.Add(splitContainerContentAndSettings);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "ChatsForm";
            Text = "GPT Chats";
            WindowState = FormWindowState.Maximized;
            splitContainerContentAndSettings.Panel1.ResumeLayout(false);
            splitContainerContentAndSettings.Panel1.PerformLayout();
            splitContainerContentAndSettings.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerContentAndSettings).EndInit();
            splitContainerContentAndSettings.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainerContentAndSettings;
        private Label lblLogitBias;
        private TextBox txtBoxLogitBias;
        private TextBox txtBoxStop;
        private Label lblStop;
        private TextBox txtBoxPresencePenalty;
        private Label lblPresencePenalty;
        private TextBox txtBoxFrequencyPenalty;
        private Label lblFrequencyPenalty;
        private TextBox txtBoxN;
        private Label lblN;
        private TextBox txtBoxTopP;
        private Label lblTopP;
        private TextBox txtBoxTemperature;
        private Label lblTemperature;
        private TextBox txtBoxTokenLimit;
        private Label lblTokenLimit;
        private TextBox txtBoxMaxResponseTokens;
        private Label lblMaxResponseTokens;
        private TextBox txtBoxModelId;
        private Label lblModelId;
        private TextBox txtBoxUsername;
        private Label lblUserName;
        private TextBox txtBoxOpenAiKey;
        private Label lblOpenAiKey;
        private TextBox txtBoxOpenAiOrganization;
        private Label lblOpenAiOrganization;
        private Label lblTokenCostFullConversation;
        private Label lblFinishReason;
        private Button BtnCloseSelectedTab;
        private Button BtnNewTab;
        private CheckBox chkShowKey;
        private CheckBox chkShowOrganization;
        private Button JavaMethod;
        private Button btnTable;
        private MenuStrip menuStrip1;
        private TabControl tabCtrlChats;
        private TextBox txtBoxFirstSystemMessage;
        private Label lblFirstSystemMessage;
    }
}