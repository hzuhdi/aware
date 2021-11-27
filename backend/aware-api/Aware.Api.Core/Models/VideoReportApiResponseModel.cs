using Aware.Api.Core.Interfaces;

namespace Aware.Api.Core.Models
{
    public class VideoReportApiResponseModel : IApiResponseModel
    {
        public string? Filename { get; set; }

        public TimeSpan ProcessingTime { get; set; }

        public double DeepfakePercentage { get; set; }        
        
    }

}
