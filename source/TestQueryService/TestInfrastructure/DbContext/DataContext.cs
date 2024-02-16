using GeneralDomain.EntityModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TestDomain.EntityModels;

namespace TestInfrastructure.DbContext;

public class DataContext:Microsoft.EntityFrameworkCore.DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DataContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    public DbSet<Routes> Routes { get; set; }
}