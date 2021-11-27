using System.Text.Json.Serialization;

namespace Aware.Api.Core.Models
{
    public class VideoReportRequest
    {
        [JsonIgnore]
        public DateTime InsertDate { get; private set; } = DateTime.UtcNow;
        
        [JsonPropertyName("filename")]
        public string Filename { get; set; }
    }
}
