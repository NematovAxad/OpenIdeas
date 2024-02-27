using GeneralDomain.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GeneralInfrastructure.DbContext;

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

    public DbSet<User> User { get; set; }
    public DbSet<Idea> Idea { get; set; }
    public DbSet<IdeaComments> IdeaComments { get; set; }
    public DbSet<IdeaFiles> IdeaFiles { get; set; }
    public DbSet<IdeaRates> IdeaRates { get; set; }
    public DbSet<SharedIdeas> SharedIdeas { get; set; }
    
}