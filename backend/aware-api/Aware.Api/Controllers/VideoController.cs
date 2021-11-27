using Aware.Api.Constants;
using Aware.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aware.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;

        public VideoController(ILogger<VideoController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Constant.Scan + "/{url}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string url, CancellationToken cancellationToken = default)
        {
            var videoResponse = new VideoResponse()
            {
                Url = url
            };
            return Ok(videoResponse); ;
        }

        [HttpPost(Constant.Scan)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(IFormFile formFile, CancellationToken cancellationToken = default)
        {
            return Ok();
        }
    }
}