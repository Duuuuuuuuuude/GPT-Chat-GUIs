namespace GPT_Chat_Desktop
{
    partial class WebView2InstallationForm
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
            lblinstallationStatus = new Label();
            SuspendLayout();
            // 
            // lblinstallationStatus
            // 
            lblinstallationStatus.AutoSize = true;
            lblinstallationStatus.Location = new Point(436, 320);
            lblinstallationStatus.Name = "lblinstallationStatus";
            lblinstallationStatus.Size = new Size(50, 20);
            lblinstallationStatus.TabIndex = 0;
            lblinstallationStatus.Text = "label1";
            // 
            // WebView2InstallationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblinstallationStatus);
            Name = "WebView2InstallationForm";
            Text = "WebView2InstallationForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblinstallationStatus;
    }
}