using GraphQL.Types;

namespace SharePosts.WebApi.Types
{
  public class ApplicationUserInputType : InputObjectGraphType
  {
    public ApplicationUserInputType()
    {
      Name = "ApplicationUserInput";
      Field<StringGraphType>("email");
      Field<StringGraphType>("password");
      Field<StringGraphType>("userName");
    }
  }
}
