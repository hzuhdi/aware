using System.Text.Json;
using Aware.Api.Core.Interfaces;
using Aware.Api.Core.Models;

namespace Aware.Api.MachineLearningClient.Clients
{
    public class VideoDeepwareDetectionClient : PythonClientBase, IPythonClient<VideoReportRequest, VideoReportResponse>
    {
        private const string ScriptFilename = "VideoDeepwareDetection.py";
        public VideoDeepwareDetectionClient() : base() { }

        public async Task<VideoReportResponse?> ExecuteAsync(VideoReportRequest requestModel, CancellationToken cancellationToken = default)
        {
            var fileDirectory = GetFilePath(ScriptFilename);

            await InitalizeEngineWithFileScript(fileDirectory, cancellationToken);

            var analyzeVideo = _scope?.GetVariable<Func<string, string>>("analyzeVideo");
            if (analyzeVideo == null)
            {
                throw new ArgumentNullException(nameof(analyzeVideo));
            }
            string? jsonString = null;

            await Task.Factory.StartNew(() => jsonString = analyzeVideo(requestModel.Filename));

            if (string.IsNullOrEmpty(jsonString)) return null;

            return JsonSerializer.Deserialize<VideoReportResponse>(jsonString);
        }
    }
}
