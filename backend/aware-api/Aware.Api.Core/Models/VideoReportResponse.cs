using System.Text.Json.Serialization;

namespace Aware.Api.Core.Models
{
    public class VideoReportResponse : VideoReportRequest
    {
        [JsonIgnore]
        public DateTime ProcessedDate { get; private set; } = DateTime.UtcNow;

        [JsonPropertyName("deepfakePercentage")]
        public double DeepfakePercentage { get; set; }

    }
}
