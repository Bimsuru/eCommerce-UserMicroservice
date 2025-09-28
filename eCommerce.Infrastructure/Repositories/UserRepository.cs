using Dapper;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly DapperDbContext _dbContext;
    public UserRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {
        // Genrate new userid
        user.UserID = Guid.NewGuid();

        string query = "INSERT INTO public.\"Users\"(\"UserID\", \"Email\", \"PersonName\", \"Gender\", \"Password\") VALUES (@UserID, @Email, @PersonName, @Gender, @Password)";

        var rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query, user);

        if (rowCountAffected > 0)
        {
            return user;
        }
        else
            return null;


    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        if (email != null && password != null)
        {
            string query = "SELECT * FROM public.\"Users\" WHERE \"Email\" = @Email AND \"Password\" = @Password";
            var parameters = new { Email = email, Password = password };

            ApplicationUser? exittingUser = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameters);

            return exittingUser;
        }
        else
            return null;
    }

    public async Task<ApplicationUser?> GetUserById(Guid? userid)
    {
        if (userid != null)
        {
            string query = "SELECT * FROM public.\"Users\" WHERE \"UserID\" = @userid";
            var parameter = new { UserID = userid };

            ApplicationUser? existingUser = await _dbContext.DbConnection.QueryFirstOrDefaultAsync<ApplicationUser>(query, parameter);

            return existingUser;
        }
        else
            return null;
    }
}
