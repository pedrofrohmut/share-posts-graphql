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
        "CreateApplicationUser",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<ApplicationUserInputType>> { Name = "newApplicationUser" }),
        resolve: context => {
          var user = context.GetArgument<ApplicationUser>("newApplicationUser");
          return applicationUserRepository.Create(user, user.Password);
        }
      );

      Field<PostType>(
        "CreatePost",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<PostInputType>> { Name = "newPost" }),
        resolve: context => postRepository.Create(context.GetArgument<Post>("newPost"))
      );

      Field<PostType>(
        "UpdatePost",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<PostInputType>> { Name = "updatedPost" }),
        resolve: context => postRepository.Update(context.GetArgument<Post>("updatedPost"))
      );

      Field<PostType>(
        "DeletePost",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "postId" }),
        resolve: context => postRepository.Delete(context.GetArgument<string>("postId"))
      );
    }
  }
}
