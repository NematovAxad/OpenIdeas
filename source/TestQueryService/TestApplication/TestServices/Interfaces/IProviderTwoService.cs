using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;

namespace TestApplication.TestServices.Interfaces;

public interface IProviderTwoService
{
    Task<SearchResponse> SearchRoute(ProviderTwoSearchRequest request);
}