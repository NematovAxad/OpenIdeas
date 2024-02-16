using TestDomain.CodeModels.Requests;
using TestDomain.CodeModels.Responses;

namespace TestApplication.TestServices.Interfaces;

public interface IProviderOneService
{
    Task<SearchResponse> SearchRoute(ProviderOneSearchRequest request);
}