using Python.Runtime;
using System.Runtime.InteropServices;
using System.Text;

namespace Middleware;

/// <summary>
/// This class is responsible for setting up the Python environment and should be called before any Python code is executed.
///
/// Should be disposed when the application not used anymore.
/// 
/// <example>
///     <code>
///     using (PythonEnvironmentSetup.getPythonEnvironmentSetupInstance())
///     {
///         new ChatsForm(new Chat());
///         new UserSettingsGlobal();
///     }
///     </code>
/// </example>
/// 
/// </summary>
public sealed class PythonEnvironmentSetup : IDisposable
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static readonly PythonEnvironmentSetup PythonEnvironmentSetupSingletonInstance = new();

    private PythonEnvironmentSetup()
    {
        #region Python Paths
        string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;

        string embeddedPythonAbsoluteShortPath = GetShortPath(Path.Combine(rootDirectory, "PythonRuntime", "python-3.11.0-embed-amd64"));
        //string embeddedPythonRelativePath = Path.GetRelativePath(rootDirectory, embeddedPythonAbsolutePath);
        //string embeddedPythonRelativePath = embeddedPythonAbsolutePath;

        string virtualPythonEnvironmentAbsoluteShortPath = GetShortPath(Path.Combine(rootDirectory, "GPT Api Client", "openai (Python 3.11 (64-bit))"));
        //string virtualPythonEnvironmentRelativPath = Path.GetRelativePath(rootDirectory, virtualPythonEnvironmentAbsolutePath);
        //string virtualPythonEnvironmentRelativPath = virtualPythonEnvironmentAbsolutePath;
        #endregion

        #region Set the runtime environment variables
        Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + embeddedPythonAbsoluteShortPath);
        Environment.SetEnvironmentVariable("PYTHONNET_PYTHON_RUNTIME", "python311");
        Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", Path.Combine(embeddedPythonAbsoluteShortPath, "python311.dll"));
        Environment.SetEnvironmentVariable("PYTHONNET_PYTHON", Path.Combine(embeddedPythonAbsoluteShortPath, "python.exe"));
        Environment.SetEnvironmentVariable("PYTHONPATH", $"{Path.Combine(embeddedPythonAbsoluteShortPath, "python311")};" +
                                                         $"{Path.Combine(rootDirectory, "GPT Api Client", "openai (Python 3.11 (64-bit))", "Lib")}");
        #endregion
        // Initialize the Python runtime
        PythonEngine.Initialize();
        PythonEngine.BeginAllowThreads();

        using (Py.GIL())
        {
            #region Appending Python paths
            dynamic sys = Py.Import("sys");

            //// Path for the 'GPT Api Client' project
            string chatModulePath = Path.Combine(rootDirectory, "GPT Api Client");
            sys.path.append(chatModulePath);

            //// Path for third party packages from the virtual Python Environment.
            string thirdPartyPythonLib = Path.Combine(virtualPythonEnvironmentAbsoluteShortPath, "Lib", "site-packages");
            sys.path.append(thirdPartyPythonLib);
            #endregion
        }
    }

    public void Dispose()
    {
        // Shutdown the Python runtime
        PythonEngine.Shutdown();
    }

    /// <summary>
    /// Returns a short form Windows path (using ~8 char segment lengths)
    /// that can help with long filenames.
    /// </summary>
    /// <remarks>
    /// IMPORTANT: File has to exist when this function is called otherwise
    /// `null` is returned.
    ///
    /// Path has to be fully qualified (no relative paths)
    /// 
    /// Max shortened file size is MAX_PATH (260) characters
    /// </remarks>
    /// <param name="path">Long Path syntax</param>
    /// <returns>Shortened 8.3 syntax or null on failure</returns>
    private static string GetShortPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        // allow for extended path syntax
        bool addExtended = false;
        if (path.Length >= 255 && !path.StartsWith(@"\\?\"))
        {
            path = @"\\?\" + path;
            addExtended = true;
        }

        var shortPath = new StringBuilder(1024);
        int res = GetShortPathName(path, shortPath, 1024);
        if (res < 1)
            return null;

        path = shortPath.ToString();

        if (addExtended)
            path = path.Substring(4);

        return path;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern int GetShortPathName(
        [MarshalAs(UnmanagedType.LPTStr)]
        string path,
        [MarshalAs(UnmanagedType.LPTStr)]
        StringBuilder shortPath,
        int shortPathLength
    );
}