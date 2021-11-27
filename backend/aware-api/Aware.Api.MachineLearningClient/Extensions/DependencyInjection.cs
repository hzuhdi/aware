using Aware.Api.Core.Interfaces;
using Aware.Api.Core.Models;
using Aware.Api.MachineLearningClient.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace Aware.Api.MachineLearningClient.Extensions
{
    public static class DependencyInjection
    {
        public static void AddMachineLearningServices(this IServiceCollection services)
        {
            services.AddSingleton<IPythonClient<VideoReportRequest, VideoReportResponse>, VideoDeepwareDetectionClient>();
        }
    }
}
