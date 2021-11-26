using System.Text.Json;
using Aware.Api.Core.Interfaces;
using Aware.Api.Core.Models;

namespace Aware.Api.MachineLearningClient.Clients
{
    public class VideoDeepwareDetectionClient : PythonClientBase, IPythonClient<VideoReportRequest, VideoReportResponse>
    {
        public VideoDeepwareDetectionClient() : base() { }

        public async Task<VideoReportResponse?> ExecuteAsync(VideoReportRequest requestModel, CancellationToken cancellationToken = default)
        {
            var script = GetScript();
            await InitializeEngineWithScript(script, cancellationToken);

            var analyzeVideo = _scope?.GetVariable<Func<string, bool, string>>("analyzeVideo");
            if (analyzeVideo == null)
            {
                throw new ArgumentNullException(nameof(analyzeVideo));
            }
            string? result = null;

            await Task.Factory.StartNew(() => result = analyzeVideo(requestModel.Url, requestModel.HasDeepfake));

            if (string.IsNullOrEmpty(result)) return null;

            return JsonSerializer.Deserialize<VideoReportResponse>(result);
        }

        private static string GetScript()
        {
            return $@"
import json
import time

class VideoAnalysisReport:
    def __init__(self, url, hasDeepfake):
      self.url = url
      self.hasDeepfake = hasDeepfake
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, 
            sort_keys=True, indent=4)
                
def analyzeVideo(url, hasDeepfake):
    time.sleep(3)
    return VideoAnalysisReport(url, hasDeepfake).toJSON()
            ";
        }
    }
}
