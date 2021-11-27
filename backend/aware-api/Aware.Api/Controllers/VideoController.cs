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
        private readonly IPythonClient<VideoReportRequest, VideoReportResponse> _client;

        public VideoController(IPythonClient<VideoReportRequest, VideoReportResponse> client, ILogger<VideoController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpPost(Constant.Scan)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(IFormFile formFile, CancellationToken cancellationToken = default)
        {
            var request = new VideoReportRequest()
            {
                Filename = formFile.FileName
            };
            
            var response = await _client.ExecuteAsync(request, cancellationToken);
            
            return Ok(response);
        }
    }
}