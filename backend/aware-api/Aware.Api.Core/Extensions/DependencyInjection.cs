using Aware.Api.Core.Interfaces;
using Aware.Api.Core.Models;
using Aware.Api.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aware.Api.Core.Extensions
{
    public static class DependencyInjection
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IDeepwareDetectionService<VideoReportApiRequestModel, VideoReportApiResponseModel>, VideoDeepwareDetectionService>();
        }
    }
}
