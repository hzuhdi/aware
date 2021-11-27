using System.Reflection;
using AutoMapper;
using Aware.Api.Core.Interfaces;
using Aware.Api.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Aware.Api.Core.Services
{
    public class VideoDeepwareDetectionService : IDeepwareDetectionService<VideoReportApiRequestModel, VideoReportApiResponseModel>
    {
        private const string FileInputFolder = "Videos";
        private readonly IPythonClient<VideoReportRequest, VideoReportResponse> _pythonClient;
        private readonly IMapper _mapper;

        public VideoDeepwareDetectionService(
            IPythonClient<VideoReportRequest, VideoReportResponse> pythonClient,
            IMapper mapper
            )
        {
            _pythonClient = pythonClient;
            _mapper = mapper;
        }


        public async Task<VideoReportApiResponseModel?> ScanAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var filename = file.FileName;
            var currentDirectory = GetCurrentInputDirectory();
            string filepath = $"{currentDirectory}\\{filename}";

            // save file
            await SaveFile(file, filepath);

            var request = new VideoReportRequest()
            {
                Filepath = filepath
            };

            var response = await GetResponse(request, cancellationToken);

            File.Delete(request.Filepath);

            return response;
        }

        private static async Task SaveFile(IFormFile file, string filepath)
        {
            if (file.Length > 0)
            {
                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

        private async Task<VideoReportApiResponseModel?> GetResponse(VideoReportRequest request, CancellationToken cancellationToken)
        {
            var response = await _pythonClient.ExecuteAsync(request, cancellationToken);
            if (response == null) return null;
            var responseModel = _mapper.Map<VideoReportResponse, VideoReportApiResponseModel>(response);
            return responseModel;
        }

        public async Task<VideoReportApiResponseModel?> ScanAsync(VideoReportApiRequestModel requestModel, CancellationToken cancellationToken = default)
        {
            return await ScanAsync(requestModel.UploadFile, cancellationToken);
        }

        private static string? GetCurrentInputDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string? currentDirectory = Path.GetDirectoryName(path);
            return $"{currentDirectory}\\Input\\{FileInputFolder}";
        }
    }
}
