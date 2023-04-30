using Python.Runtime;

namespace Middleware;

internal sealed class PythonEnvironmentSetup : IDisposable
{
    internal PythonEnvironmentSetup()
    {
        #region Python Paths
        string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;

        string embeddedPythonAbsolutePath = Path.Combine(rootDirectory, "PythonRuntime", "python-3.11.0-embed-amd64");
        string embeddedPythonRelativePath = Path.GetRelativePath(rootDirectory, embeddedPythonAbsolutePath);
        //string embeddedPythonRelativePath = embeddedPythonAbsolutePath;

        string virtualPythonEnvironmentAbsolutePath = Path.Combine(rootDirectory, "GPT Api Client", "openai GPT (Python 3.11 (64-bit))");
        string virtualPythonEnvironmentRelativPath = Path.GetRelativePath(rootDirectory, virtualPythonEnvironmentAbsolutePath);
        //string virtualPythonEnvironmentRelativPath = virtualPythonEnvironmentAbsolutePath;
        #endregion

        #region Set the runtime environment variables
        Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + embeddedPythonRelativePath);
        Environment.SetEnvironmentVariable("PYTHONNET_PYTHON_RUNTIME", "python311");
        Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", Path.Combine(embeddedPythonRelativePath, "python311.dll"));
        Environment.SetEnvironmentVariable("PYTHONNET_PYTHON", Path.Combine(embeddedPythonRelativePath, "python.exe"));
        Environment.SetEnvironmentVariable("PYTHONPATH", $"{Path.Combine(embeddedPythonRelativePath, "python311")};" +
                                                         $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GPT Api Client", "openai GPT (Python 3.11 (64-bit))", "Lib")}");
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
            string thirdPartyPythonLib = Path.Combine(virtualPythonEnvironmentRelativPath, "Lib", "site-packages");
            sys.path.append(thirdPartyPythonLib);
            #endregion
        }
    }

    public void Dispose()
    {
        // Shutdown the Python runtime
        PythonEngine.Shutdown();
    }
}