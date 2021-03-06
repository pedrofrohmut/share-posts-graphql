using GraphQL.Types;
using SharePosts.DataBase.Entities;

namespace SharePosts.WebApi.Types
{
  public class ApplicationUserType : ObjectGraphType<ApplicationUser>
  {
    public ApplicationUserType()
    {
      Name = "ApplicationUser";
      Field(x => x.Id);
      Field(x => x.UserName);
      Field(x => x.Email);
      Field(x => x.EmailConfirmed);
      Field(x => x.AuthenticationToken);
    }
  }
}
