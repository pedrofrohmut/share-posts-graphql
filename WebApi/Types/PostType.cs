using GraphQL.Types;
using SharePosts.DataAccess.Repositories;
using SharePosts.DataBase.Entities;

namespace SharePosts.WebApi.Types
{
  public class PostType : ObjectGraphType<Post>
  {
    public PostType(IApplicationUsersRepository applicationUserRepository)
    {
      Name = "Post";
      Field(post => post.Id);
      Field(post => post.Title);
      Field(post => post.Body);
      Field(post => post.CreatedAt);
      Field(post => post.ApplicationUserId);
      Field<ApplicationUserType>(
        "Author",
        resolve: context => 
          applicationUserRepository.FindById(context.Source.ApplicationUserId)
      );
    }
  }
}
