using System.Text.Json;
using Aware.Api.Core.Interfaces;
using Aware.Api.Core.Models;
using IronPython.Hosting;

namespace Aware.Api.MachineLearningClient.Clients
{
    public class VideoDeepwareDetectionClient : PythonClientBase, IPythonClient<VideoReportRequest, VideoReportResponse>
    {
        private const string ScriptFilename = "VideoDeepwareDetection.py";
        public VideoDeepwareDetectionClient() : base() { }

        public async Task<VideoReportResponse?> ExecuteAsync(VideoReportRequest requestModel, CancellationToken cancellationToken = default)
        {
            var fileDirectory = GetFilePath(ScriptFilename);

            var jsonString = await RunIronPython(requestModel, fileDirectory);

            //var script = GetScript();
            //await InitializeEngineWithScript(script, cancellationToken);

            //var analyzeVideo = _scope?.GetVariable<Func<string, string>>("analyzeVideo");
            //if (analyzeVideo == null)
            //{
            //    throw new ArgumentNullException(nameof(analyzeVideo));
            //}
            //string? result = null;

            //await Task.Factory.StartNew(() => result = analyzeVideo(requestModel.Filename));

            if (string.IsNullOrEmpty(jsonString)) return null;

            return JsonSerializer.Deserialize<VideoReportResponse>(jsonString);
        }

        private async Task<string?> RunIronPython(VideoReportRequest requestModel, string fileDirectory)
        {
            var source = _engine.CreateScriptSourceFromFile(fileDirectory);
            var scope = _engine.CreateScope();
            Microsoft.Scripting.Hosting.ScriptScope scriptScope = _engine.GetSysModule();
            //  scriptScope.
            scriptScope.SetVariable("filename", requestModel.Filename);
            var argv = new List<string>();
            //Do some stuff and fill argv
            argv.Add(requestModel.Filename);
            _engine.GetSysModule().SetVariable("argv", argv);
            string? jsonString = null;
            await Task.Factory.StartNew(() => jsonString = source.Execute<string>(scope));
            return jsonString;
        }
        public async Task<string?> RunCmd(VideoReportRequest requestModel, string fileDirectory)
        {
            // https://stackoverflow.com/questions/53080120/passing-variable-between-c-sharp-and-python-ironpython
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"python {fileDirectory} {requestModel.Filename}";
            //"/C copy /b Image1.jpg + Archive.rar Image2.jpg";
            process.StartInfo = startInfo;
            process.Start();
            return await process.StandardOutput.ReadToEndAsync();
        }
    }
}
