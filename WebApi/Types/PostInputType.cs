using GraphQL.Types;

namespace SharePosts.WebApi.Types
{
  public class PostInputType : InputObjectGraphType
  {
    public PostInputType()
    {
      Name = "PostInput";
      Field<StringGraphType>("id");
      Field<StringGraphType>("title");
      Field<StringGraphType>("body");
    }
  }
}
