using Microsoft.Web.WebView2.Core;
using Middleware;

namespace GPT_Chat_Desktop;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        if (IsWebView2RuntimeInstalled())
        {
            using (PythonEnvironmentSetup.PythonEnvironmentSetupSingletonInstance)
            {
                Application.Run(new ChatsForm(new Chat(), new UserSettingsGlobal()));
            }

        }
        else
        {
            using (WebView2InstallationForm installationForm = new WebView2InstallationForm())
            {
                if (installationForm.ShowDialog() == DialogResult.OK)
                {
                    using (PythonEnvironmentSetup.PythonEnvironmentSetupSingletonInstance)
                    {
                        Application.Run(new ChatsForm(new Chat(), new UserSettingsGlobal()));
                    }
                }
            }
        }
    }

    private static bool IsWebView2RuntimeInstalled()
    {
        try
        {
            CoreWebView2Environment.GetAvailableBrowserVersionString();
            return true;
        }
        catch (WebView2RuntimeNotFoundException)
        {
            return false;
        }
    }

}