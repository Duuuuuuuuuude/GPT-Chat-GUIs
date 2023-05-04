// WebView2InstallationForm.cs
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Security.Principal;

namespace GPT_Chat_Desktop
{
    public partial class WebView2InstallationForm : Form
    {
        private const string BootstrapperUrl = "https://go.microsoft.com/fwlink/p/?LinkId=2124703";
        private static readonly string BootstrapperPath = Path.Combine(Path.GetTempPath(), "MicrosoftEdgeWebview2Setup.exe");

        private Label installationStatusLabel;

        public WebView2InstallationForm()
        {
            InitializeComponent();

            // Add a label to show installation status
            installationStatusLabel = new Label { Text = "Installing WebView2 Runtime...", Dock = DockStyle.Fill };
            Controls.Add(installationStatusLabel);

            Load += WebView2InstallationForm_Load;
        }

        private void WebView2InstallationForm_Load(object sender, EventArgs e)
        {
            if (IsUserAdministrator())
            {
                EnsureWebView2Runtime();
            }
            else
            {
                MessageBox.Show("Error: This openai chat GUI requires WebView2 Runtime to be installed, installation requires administrative privileges. Start this program again as an admin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void EnsureWebView2Runtime()
        {
            // WebView2 Runtime not installed, download and install it silently
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                webClient.DownloadFileAsync(new Uri(BootstrapperUrl), BootstrapperPath);
            }
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Error downloading WebView2 Bootstrapper: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                ProcessStartInfo psi = new ProcessStartInfo(BootstrapperPath)
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    Arguments = "/silent /install"
                };

                try
                {
                    Process.Start(psi);
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error running WebView2 Bootstrapper: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                }
            }
            Close();
        }

        private bool IsUserAdministrator()
        {
            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch (Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }
    }
}