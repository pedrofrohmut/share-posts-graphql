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
        "SignUp",
        arguments: new QueryArguments(
          new QueryArgument<StringGraphType> { Name = "userName" },
          new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "email" },
          new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" }),
        resolve: context => 
        {
          var userName = context.GetArgument<string>("userName");
          var email = context.GetArgument<string>("email");
          var password = context.GetArgument<string>("password");
          return applicationUserRepository.Create(userName, email, password);
        }
      );

      Field<ApplicationUserType>(
        "SignIn",
        arguments: new QueryArguments(
          new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "email" },
          new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "password" }),
        resolve: context => 
        {
          var email = context.GetArgument<string>("email");
          var password = context.GetArgument<string>("password");
          return applicationUserRepository.GetAuthenticatedUser(email, password);
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
