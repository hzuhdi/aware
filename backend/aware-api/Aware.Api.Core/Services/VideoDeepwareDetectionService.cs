using System.Reflection;
using Aware.Api.Core.Interfaces;
using Aware.Api.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Aware.Api.Core.Services
{
    public class VideoDeepwareDetectionService : IDeepwareDetectionService<VideoReportApiRequestModel, VideoReportApiResponseModel>
    {
        private const string FileInputFolder = "Videos";
        private readonly IPythonClient<VideoReportRequest, VideoReportResponse> _pythonClient;

        public VideoDeepwareDetectionService(IPythonClient<VideoReportRequest, VideoReportResponse> pythonClient)
        {
            _pythonClient = pythonClient;
        }

        public async Task<VideoReportApiResponseModel?> ScanAsync(VideoReportApiRequestModel requestModel, CancellationToken cancellationToken = default) 
            => await ScanAsync(requestModel.UploadFile, cancellationToken);

        public async Task<VideoReportApiResponseModel?> ScanAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var filename = file.FileName;
            var currentDirectory = GetCurrentInputDirectory();
            string filepath = $"{currentDirectory}\\{filename}";

            // Save File
            await SaveFile(file, filepath);

            // Execute ML model against saved file
            var request = new VideoReportRequest();
            request.Filepath = filepath;

            var response = await _pythonClient.ExecuteAsync(request, cancellationToken);
            if (response == null) return null;
            var responseModel = new VideoReportApiResponseModel()
            {
                Filename = filename,
                DeepfakePercentage = response.DeepfakePercentage,
                ProcessingTime = response.ProcessedDate - response.InsertDate,
            };

            // Delete saved file
            File.Delete(request.Filepath);

            return responseModel;
        }

        private static async Task SaveFile(IFormFile file, string filepath)
        {
            if (file.Length <= 0) return;
            using Stream fileStream = new FileStream(filepath, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        private static string? GetCurrentInputDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string? currentDirectory = Path.GetDirectoryName(path);

            currentDirectory += "\\Input";
            CreateDirectoryIfExists(currentDirectory);

            currentDirectory += $"\\{FileInputFolder}";
            CreateDirectoryIfExists(currentDirectory);

            return currentDirectory;
        }

        private static void CreateDirectoryIfExists(string? currentDirectory)
        {
            if (string.IsNullOrEmpty(currentDirectory)) return;
            if (Directory.Exists(currentDirectory)) return;
            Directory.CreateDirectory(currentDirectory);
        }
    }
}
