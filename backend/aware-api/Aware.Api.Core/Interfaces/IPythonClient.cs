namespace Aware.Api.Core.Interfaces
{
    public interface IPythonClient<TRequestModel, TResponseModel>
        where TRequestModel : class
        where TResponseModel : class
    {
        Task<TResponseModel?> ExecuteAsync(TRequestModel requestModel, CancellationToken cancellationToken);
    }
}
