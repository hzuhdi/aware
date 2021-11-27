using Microsoft.AspNetCore.Http;

namespace Aware.Api.Core.Interfaces
{
    public interface IDeepwareDetectionService<TApiRequestModel, TApiResponseModel>
        where TApiRequestModel : IApiRequestModel
        where TApiResponseModel : IApiResponseModel
    {
        Task<TApiResponseModel?> ScanAsync(TApiRequestModel requestModel, CancellationToken cancellationToken);

        Task<TApiResponseModel?> ScanAsync(IFormFile file, CancellationToken cancellationToken);
    }
}
