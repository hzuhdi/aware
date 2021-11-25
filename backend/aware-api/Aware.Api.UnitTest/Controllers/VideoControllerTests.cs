using System;
using System.Threading.Tasks;
using Aware.Api.Controllers;
using Aware.Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Aware.Api.UnitTest.Controllers
{
    public class VideoControllerTests
    {
        private VideoController _controller;
        
        public VideoControllerTests()
        {
            _controller = new VideoController(Mock.Of<ILogger<VideoController>>());
        }

        [Fact]
        public async Task Get_WithValidUrl_ReturnsOk()
        {
            var url = "https://www.google.com/";

            var controllerResult = await _controller.Get(url);

            controllerResult.Should().BeOfType<OkObjectResult>();

            var result = controllerResult as OkObjectResult;
            result?.Value.Should().BeOfType<VideoResponse>();

            var videoResponse = result?.Value as VideoResponse;
            videoResponse?.Url.Should().Be(url);
        }
    }
}