using GraphQL.Types;
using SharePosts.DataBase.Entities;

namespace SharePosts.WebApi.Types
{
  public class PostType : ObjectGraphType<Post>
  {
    public PostType()
    {
      Field(post => post.Id);
      Field(post => post.Title);
      Field(post => post.Body);
      Field(post => post.CreatedAt);
      // TODO: relation to the author
    }
  }
}
