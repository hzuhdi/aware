namespace Aware.Api.Core.Interfaces
{
    public interface IPythonClient<TReportRequest, TReportResponse>
        where TReportRequest : IReportRequest
        where TReportResponse : IReportResponse
    {
        Task<TReportResponse?> ExecuteAsync(TReportRequest requestModel, CancellationToken cancellationToken);
    }
}
