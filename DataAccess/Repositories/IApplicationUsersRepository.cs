using System.Collections.Generic;
using System.Threading.Tasks;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories
{
  public interface IApplicationUsersRepository
  {
    /*
     * Mutation: Create a new user with email and password
     */
    Task<ApplicationUser> Create(ApplicationUser newUser, string password);

    /*
     * Query: Find Application User with an email parameter
     */
    Task<ApplicationUser> FindByEmail(string email);

    /*
     * Query: Find Application User by its Id
     */
    Task<ApplicationUser> FindById(string id);

    /*
     * Query: Get All ApplicationUsers
     */
    Task<IEnumerable<ApplicationUser>> GetAll();

    /*
     * Query: Get Authentication Token passing email and password (Sign In)
     */
    Task<string> GetAuthenticationToken(string email, string password);
  }
}
