using Aware.Api.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Aware.Api.Core.Models
{
    public class VideoReportApiRequestModel : IApiRequestModel
    {
        public IFormFile? UploadFile { get; set; }
    }

}
