// WebView2InstallationForm.cs
using System.Diagnostics;
using System.Security.Principal;

namespace GPT_Chat_Desktop
{
    public partial class WebView2InstallationForm : Form
    {
        private const string BootstrapperUrl = "https://go.microsoft.com/fwlink/p/?LinkId=2124703";
        private static readonly string BootstrapperPath = Path.Combine(Path.GetTempPath(), "MicrosoftEdgeWebview2Setup.exe");

        public WebView2InstallationForm()
        {
            InitializeComponent();

            Load += async (sender, e) => await WebView2InstallationForm_LoadAsync(sender, e);
        }

        private async Task WebView2InstallationForm_LoadAsync(object sender, EventArgs e)
        {
            if (IsUserAdministrator())
            {
                await DownloadWebView2RuntimeAsync();
            }
            else
            {
                MessageBox.Show("Error: This openai chat GUI requires WebView2 Runtime to be installed, installation requires administrative privileges. Start this program again as an admin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private async Task DownloadWebView2RuntimeAsync()
        {
            try
            {
                lblinstallationStatus.Text = "Downloading WebView2 Runtime...";

                // WebView2 Runtime not installed, download and install it silently
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(BootstrapperUrl);
                    response.EnsureSuccessStatusCode();

                    using (var fileStream = new FileStream(BootstrapperPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fileStream);
                    }
                }

                WebClient_DownloadFileCompleted(true, null);
            }
            catch (HttpRequestException e)
            {
                WebClient_DownloadFileCompleted(false, e.Message);
            }


        }


        private void WebClient_DownloadFileCompleted(bool success, string errorMessage)
        {
            if (!success)
            {
                lblinstallationStatus.Text = "Failed to downloading WebView2 Runtime...";
                MessageBox.Show("Error downloading WebView2 Bootstrapper: " + errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
                return;
            }

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
                MessageBox.Show("Error running WebView2 Bootstrapper: " + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                DialogResult = DialogResult.Cancel;
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