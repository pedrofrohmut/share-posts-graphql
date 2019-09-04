using System.Collections.Generic;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories.Implementations
{
  public class ApplicationUsersRepository : IApplicationUsersRepository
  {
    public void Create(ApplicationUser newUser)
    {
      throw new System.NotImplementedException();
    }

    public ApplicationUser FindByEmail(string email)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<ApplicationUser> GetAll()
    {
      throw new System.NotImplementedException();
    }

    public string GetAuthenticationToken(string email, string password)
    {
      throw new System.NotImplementedException();
    }
  }
}
