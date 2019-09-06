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
      Name = "Query";

      Field<ListGraphType<ApplicationUserType>>(
        "AllApplicationUser",
        resolve: context => applicationUserRepository.GetAll()
      );

      Field<ApplicationUserType>(
        "ApplicationUser",
        arguments: new QueryArguments(
          new QueryArgument<StringGraphType> { Name = "email" }),
        resolve: context => applicationUserRepository
          .FindByEmail(context.GetArgument<string>("email"))
      );

      Field<ListGraphType<PostType>>(
        "AllPost",
        resolve: context => postsRepository.GetAll()
      );

      Field<PostType>(
        "Post",
        arguments: new QueryArguments(
          new QueryArgument<StringGraphType> { Name = "id" }),
        resolve: context => 
          postsRepository.FindById(context.GetArgument<string>("id"))
      );
    }
  }
}
