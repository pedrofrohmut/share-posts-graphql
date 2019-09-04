using GraphQL.Types;
using SharePosts.DataAccess.Repositories;
using SharePosts.WebApi.Types;

namespace SharePosts.WebApi.Queries
{
  public class RootQuery : ObjectGraphType
  {
    public RootQuery(
        IPostsRepository postsRepository,
        IApplicationUsersRepository applicationUserRepository)
    {
      Field<ListGraphType<ApplicationUserType>>(
        "allApplicationUser",
        resolve: (context) => applicationUserRepository.GetAll()
      );

      Field<ApplicationUserType>(
        "applicationUser",
        arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "email" }),
        resolve: (context) => applicationUserRepository.FindByEmail(context.GetArgument<string>("email"))
      );
    }
  }
}
