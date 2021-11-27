using System.Reflection;

namespace Aware.Api.MachineLearningClient.Clients
{
    public abstract class PythonClientBase
    {
        protected Microsoft.Scripting.Hosting.ScriptEngine? _engine;
        protected Microsoft.Scripting.Hosting.ScriptScope? _scope;

        public PythonClientBase()
        {
            _engine = IronPython.Hosting.Python.CreateEngine();
            _scope = _engine.CreateScope();
        }

        protected string GetFilePath(string filename)
        {
            string ScriptFolderName = "Scripts";
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string? currentDirectory = Path.GetDirectoryName(path);
            return $"{currentDirectory}\\{ScriptFolderName}\\{filename}";
        }

        protected async Task InitializeEngineWithScript(string script, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(script)) throw new ArgumentNullException(nameof(script));
            await Task.Factory.StartNew(() => _engine?.Execute(script, _scope), cancellationToken);
        }

        protected async Task InitalizeEngineWithFileScript(string filepath, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(filepath)) throw new ArgumentNullException(nameof(filepath));
            await Task.Factory.StartNew(() => _engine?.ExecuteFile(filepath, _scope), cancellationToken);
        }
    }
}
