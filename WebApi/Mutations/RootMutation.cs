using GraphQL.Types;
using SharePosts.DataAccess.Repositories;
using SharePosts.DataBase.Entities;
using SharePosts.WebApi.Types;

namespace SharePosts.WebApi.Mutations
{
  public class RootMutation : ObjectGraphType
  {
    public RootMutation(
        IApplicationUsersRepository applicationUserRepository,
        IPostsRepository postRepository)
    {
      Name = "Mutation";

      Field<ApplicationUserType>(
        "createApplicationUser",
        arguments: new QueryArguments(
          new QueryArgument<ApplicationUserInputType> { Name = "newApplicationUser" }),
        resolve: context => {
          var user = context.GetArgument<ApplicationUser>("newApplicationUser");
          return applicationUserRepository.Create(user, user.Password);
        }
      );

      Field<PostType>(
        "createPost",
        arguments: new QueryArguments(
          new QueryArgument<PostInputType> { Name = "newPost" }),
        resolve: context => {
          var newPost = context.GetArgument<Post>("newPost");
          return postRepository.Create(newPost);
        }
      )
    }
  }
}
