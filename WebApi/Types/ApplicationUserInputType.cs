using GraphQL.Types;

namespace SharePosts.WebApi.Types
{
  public class ApplicationUserInputType : InputObjectGraphType
  {
    public ApplicationUserInputType()
    {
      Name = "ApplicationUserInput";
      Field<NonNullGraphType<StringGraphType>>("email");
      Field<NonNullGraphType<StringGraphType>>("password");
      Field<StringGraphType>("userName");
    }
  }
}
