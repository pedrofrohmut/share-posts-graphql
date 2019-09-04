using System.Collections.Generic;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories
{
  public interface IApplicationUsersRepository
  {
    /*
     * Mutation: Create a new user with email and password
     */
    void Create(ApplicationUser newUser);

    /*
     * Query: Find Application User with an email parameter
     */
    ApplicationUser FindByEmail(string email);

    /*
     * Query: Get All ApplicationUsers
     */
    IEnumerable<ApplicationUser> GetAll();

    /*
     * Query: Get Authentication Token passing email and password (Sign In)
     */
    string GetAuthenticationToken(string email, string password);
  }
}
