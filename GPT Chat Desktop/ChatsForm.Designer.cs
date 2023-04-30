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
            BtnCloseSelectedTab = new Button();
            BtnNewTab = new Button();
            tabCtrlChats = new TabControl();
            ((System.ComponentModel.ISupportInitialize)splitContainerContentAndSettings).BeginInit();
            splitContainerContentAndSettings.Panel1.SuspendLayout();
            splitContainerContentAndSettings.Panel2.SuspendLayout();
            splitContainerContentAndSettings.SuspendLayout();
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
            splitContainerContentAndSettings.Panel1.Controls.Add(BtnCloseSelectedTab);
            splitContainerContentAndSettings.Panel1.Controls.Add(BtnNewTab);
            // 
            // splitContainerContentAndSettings.Panel2
            // 
            splitContainerContentAndSettings.Panel2.Controls.Add(tabCtrlChats);
            splitContainerContentAndSettings.Size = new Size(800, 450);
            splitContainerContentAndSettings.SplitterDistance = 266;
            splitContainerContentAndSettings.TabIndex = 0;
            // 
            // BtnCloseSelectedTab
            // 
            BtnCloseSelectedTab.Location = new Point(86, 76);
            BtnCloseSelectedTab.Name = "BtnCloseSelectedTab";
            BtnCloseSelectedTab.Size = new Size(123, 29);
            BtnCloseSelectedTab.TabIndex = 1;
            BtnCloseSelectedTab.Text = "&Close Selected Tab";
            BtnCloseSelectedTab.UseVisualStyleBackColor = true;
            BtnCloseSelectedTab.Click += BtnCloseTab_Click;
            // 
            // BtnNewTab
            // 
            BtnNewTab.Location = new Point(86, 29);
            BtnNewTab.Name = "BtnNewTab";
            BtnNewTab.Size = new Size(94, 29);
            BtnNewTab.TabIndex = 0;
            BtnNewTab.Text = "&New Tab";
            BtnNewTab.UseVisualStyleBackColor = true;
            BtnNewTab.Click += BtnNewTab_Click;
            // 
            // tabCtrlChats
            // 
            tabCtrlChats.Dock = DockStyle.Fill;
            tabCtrlChats.Location = new Point(0, 0);
            tabCtrlChats.Name = "tabCtrlChats";
            tabCtrlChats.SelectedIndex = 0;
            tabCtrlChats.Size = new Size(530, 450);
            tabCtrlChats.TabIndex = 0;
            // 
            // ChatsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainerContentAndSettings);
            Name = "ChatsForm";
            Text = "GPT Chats";
            splitContainerContentAndSettings.Panel1.ResumeLayout(false);
            splitContainerContentAndSettings.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerContentAndSettings).EndInit();
            splitContainerContentAndSettings.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainerContentAndSettings;
        private Button BtnCloseSelectedTab;
        private Button BtnNewTab;
        private TabControl tabCtrlChats;
    }
}