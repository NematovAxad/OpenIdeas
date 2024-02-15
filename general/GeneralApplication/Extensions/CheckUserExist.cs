using GeneralInfrastructure.DbContext;

namespace GeneralApplication.Extensions;

public class CheckUserExist
{
    private readonly DataContext _dbContext;

    public CheckUserExist(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool CheckById(int id)
    {
        var user = _dbContext.User.FirstOrDefault(u => u.Id == id);

        return user != null;
    }
}