using TestDomain.CodeModels.Responses;
using TestDomain.EntityModels;

namespace TestDomain.Repository;

public interface ICacheRepository
{
    Task<RouteModel?> GetByIdAsync(string key, CancellationToken cancellationToken = default);

    Task AddAsync(RouteModel entity, CancellationToken cancellationToken = default);
}