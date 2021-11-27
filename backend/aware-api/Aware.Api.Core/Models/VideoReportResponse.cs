using System.Text.Json.Serialization;
using Aware.Api.Core.Interfaces;

namespace Aware.Api.Core.Models
{
    public class VideoReportResponse : VideoReportRequest, IReportResponse
    {
        [JsonIgnore]
        public DateTime ProcessedDate { get; private set; } = DateTime.UtcNow;

        [JsonPropertyName("deepfakePercentage")]
        public double DeepfakePercentage { get; set; }

    }
}
