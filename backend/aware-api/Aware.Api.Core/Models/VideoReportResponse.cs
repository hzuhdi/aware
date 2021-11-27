using System.Text.Json.Serialization;
using Aware.Api.Core.Interfaces;

namespace Aware.Api.Core.Models
{
    public class VideoReportResponse : VideoReportRequest, IReportResponse
    {
        [JsonIgnore]
        public DateTime ProcessedDate { get; private set; } = DateTime.UtcNow;

        [JsonPropertyName("percentage")]
        public double DeepfakePercentage { get; set; }

        [JsonPropertyName("filename")]
        public string? Filename { get; set; }

        public string Description => 
            DeepfakePercentage >= 50 ? 
            "Deepfake detected.":"This is a real video.";

    }
}
