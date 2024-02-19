using GeneralDomain.Responses;
using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;

namespace TestApplication.TestServices.Interfaces;

public interface ISearchService
{
    Task<Response<SearchResponse>> SearchRoute(SearchRequest request);
    Task<Response<SearchResponse>> GetCachedData();
    Task<Response<SearchResponse>> GetByIdFromCache(Guid guid);
    Task<Response<bool>> IsServiceAvailable();
}