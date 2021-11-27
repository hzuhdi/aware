using System;
using System.Threading;
using System.Threading.Tasks;
using Aware.Api.Controllers;
using Aware.Api.Core.Interfaces;
using Aware.Api.Core.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Aware.Api.UnitTest.Controllers
{
    public class VideoControllerTests
    {
        private Mock<IDeepwareDetectionService<VideoReportApiRequestModel, VideoReportApiResponseModel>> _service;
        private VideoController _controller;

        public VideoControllerTests()
        {
            _service = new Mock<IDeepwareDetectionService<VideoReportApiRequestModel, VideoReportApiResponseModel>>();
            _controller = new VideoController(_service.Object, Mock.Of<ILogger<VideoController>>());
        }

        [Fact]
        public async Task Scan_ReturnsOk()
        {
            // Arrange
            string filename = Guid.NewGuid().ToString();

            var dfPercentage = new Random().NextDouble();

            var item = new Mock<IFormFile>();
            item.Setup(i => i.FileName).Returns(filename);

            _service.Setup(x => x.ScanAsync(It.IsAny<VideoReportApiRequestModel>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new VideoReportApiResponseModel() { Filename = filename, DeepfakePercentage = dfPercentage});


            // Act
            var result = await _controller.Post(item.Object);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
            okResult.Value.Should().BeOfType<VideoReportResponse>();
            var response = (VideoReportResponse)okResult.Value;
            response.Should().NotBeNull();
            response.Filepath.Should().Be(filename);
            response.DeepfakePercentage.Should().Be(dfPercentage);
            response.ProcessedDate.Should().BeAfter(response.InsertDate);
        }
    }
}