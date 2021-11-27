using System.Text.Json.Serialization;
using Aware.Api.Core.Interfaces;

namespace Aware.Api.Core.Models
{
    public class VideoReportRequest : IReportRequest
    {
        [JsonIgnore]
        public DateTime InsertDate { get; private set; } = DateTime.UtcNow;

        [JsonPropertyName("filename")]
        public string? Filepath { get; set; }
    }
}
