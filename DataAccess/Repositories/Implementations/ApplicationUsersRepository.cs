using System.Collections.Generic;
using System.Linq;
using SharePosts.DataBase.Context;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories.Implementations
{
  public class ApplicationUsersRepository : IApplicationUsersRepository
  {
    private readonly SharePostsDbContext context;

    public ApplicationUsersRepository(SharePostsDbContext context)
    {
      this.context = context;
    }

    public void Create(ApplicationUser newUser)
    {
      throw new System.NotImplementedException();
    }

    public ApplicationUser FindByEmail(string email)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<ApplicationUser> GetAll() => 
      this.context.ApplicationUsers.ToList();

    public string GetAuthenticationToken(string email, string password)
    {
      throw new System.NotImplementedException();
    }
  }
}
