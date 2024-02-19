using TestDomain.CodeModels.Responses;
using TestDomain.EntityModels;

namespace TestDomain.Repository;

public interface ICacheRepository
{
    Task<RouteModel?> GetByIdAsync(string key);
    
    Task<List<RouteModel?>?> GetAllAsync();

    Task AddAsync(RouteModel entity);
}