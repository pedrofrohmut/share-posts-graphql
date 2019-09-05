using GraphQL.Types;
using SharePosts.DataAccess.Repositories;
using SharePosts.DataBase.Entities;
using SharePosts.WebApi.Types;

namespace SharePosts.WebApi.Mutations
{
  public class RootMutation : ObjectGraphType
  {
    public RootMutation(IApplicationUsersRepository repo)
    {
      Field<ApplicationUserType>
      (
        "createApplicationUser",
        arguments: new QueryArguments
        (
          new QueryArgument<ApplicationUserInputType> { Name = "newApplicationUser" }
        ),
        resolve: context =>
        {
          var user = context.GetArgument<ApplicationUser>("newApplicationUser");
          return repo.Create(user, user.Password);
        }
      );
    }
  }
}
