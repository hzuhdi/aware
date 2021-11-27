﻿using System.Text.Json.Serialization;

namespace Aware.Api.Core.Models
{
    public class VideoReportRequest
    {
        [JsonIgnore]
        public DateTime InsertDate { get; private set; } = DateTime.UtcNow;
        
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("hasDeepfake")]
        public bool HasDeepfake { get; set; }

    }
}
