using Aware.Api.Attributes;
using Aware.Api.Constants;
using Aware.Api.Core.Interfaces;
using Aware.Api.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aware.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;
        private readonly IDeepwareDetectionService<VideoReportApiRequestModel, VideoReportApiResponseModel> _detectionService;

        public VideoController(
            IDeepwareDetectionService<VideoReportApiRequestModel, VideoReportApiResponseModel> detectionService,
            ILogger<VideoController> logger)
        {
            _detectionService = detectionService;
            _logger = logger;
        }

        [HttpPost(Constant.Scan)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([AllowedExtensions(new[] { ".mp4", ".avi" })] IFormFile formFile, CancellationToken cancellationToken = default)
        {
            var response = await _detectionService.ScanAsync(formFile, cancellationToken);
            
            return Ok(response);
        }
    }
}