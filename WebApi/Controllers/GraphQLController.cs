using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using SharePosts.WebApi.Utils;

namespace SharePosts.WebApi.Controllers
{
  [Route("/graphql")]
  [ApiController]
  public class GraphQLController : ControllerBase
  {
    private readonly ISchema schema;
    private readonly IDocumentExecuter documentExecuter;

    public GraphQLController(
        ISchema schema,
        IDocumentExecuter documentExecuter)
    {
      this.schema = schema;
      this.documentExecuter = documentExecuter;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] GraphQLQuery requestQuery)
    {
      if (requestQuery == null)
      {
        return BadRequest(new
        {
          message = "The Query cannot be null. You must inform a graphql query in the POST request body"
        });
      }
      var inputs = requestQuery.Variables?.ToInputs();
      var executionOptions = new ExecutionOptions
      {
        Schema = this.schema,
        Query = requestQuery.Query,
        Inputs = inputs
      };
      var result = await this.documentExecuter.ExecuteAsync(executionOptions);
      if (result.Errors?.Count > 0)
      {
        return BadRequest(new { message = result });
      }
      return Ok(result);
    }

  }
}
