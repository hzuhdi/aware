using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aware.Api.Core.Models
{
    public class VideoReportResponse : VideoReportRequest
    {
        public DateTime ProcessedDate { get; private set; } = DateTime.UtcNow;
    }
}
