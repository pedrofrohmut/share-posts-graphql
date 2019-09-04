using GraphQL;
using SharePosts.WebApi.Queries;

namespace SharePosts.WebApi.Schema
{
  public class SharePostsSchema : GraphQL.Types.Schema
  {
    public SharePostsSchema(IDependencyResolver resolver) : base(resolver)
    {
      Query = resolver.Resolve<RootQuery>();
    }
  }
}
