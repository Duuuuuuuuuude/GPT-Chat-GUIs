using Microsoft.Web.WebView2.Core;
using Middleware;

namespace GPT_Chat_Desktop;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static async Task Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        if (IsWebView2RuntimeInstalled())
        {
            using (PythonEnvironmentSetup.GetSingletonInstance)
            {
                Application.Run(await ChatsForm.ChatsFormAsync(new UserSettingsGlobal(), typeof(Chat)));
            }
        }
        else
        {
            using (WebView2InstallationForm installationForm = new())
            {
                if (installationForm.ShowDialog() == DialogResult.OK)
                {
                    using (PythonEnvironmentSetup.GetSingletonInstance)
                    {
                        Application.Run(await ChatsForm.ChatsFormAsync(new UserSettingsGlobal(), typeof(Chat)));
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