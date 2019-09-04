using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharePosts.DataBase.Context;
using SharePosts.DataBase.Entities;
using SharePosts.WebApi.Utils;

namespace SharePosts.WebApi.Controllers
{
  [Route("/graphql")]
  [ApiController]
  public class GraphQLController : ControllerBase
  {
    private readonly ISchema schema;
    private readonly IDocumentExecuter documentExecuter;
    private readonly SharePostsDbContext dbContext;
    private readonly UserManager<ApplicationUser> userManager;

    public GraphQLController(
        ISchema schema,
        IDocumentExecuter documentExecuter,
        SharePostsDbContext dbContext,
        UserManager<ApplicationUser> userManager)
    {
      this.schema = schema;
      this.documentExecuter = documentExecuter;
      this.dbContext = dbContext;
      this.userManager = userManager;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] GraphQLQuery requestQuery)
    {
      if (requestQuery == null)
      {
        return BadRequest(new
        {
          message =
            "The Query cannot be null. You must inform a graphql query in the POST request body"
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

    [HttpGet("seed_database")]
    public async Task<ActionResult> Seed()
    {
      var password = "1234";
      if (!this.dbContext.ApplicationUsers.Any())
      {

        await this.userManager.CreateAsync(new ApplicationUser
        {
          UserName = "JohnDoe",
          Email = "john@doe.com"
        }, password);
        await this.userManager.CreateAsync(new ApplicationUser
        {
          UserName = "JaneDoe",
          Email = "jane@doe.com"
        }, password);
        await this.userManager.CreateAsync(new ApplicationUser
        {
          UserName = "BobSmith",
          Email = "bob@smith.com"
        }, password);
        var john = await this.userManager.FindByEmailAsync("john@doe.com");
        var jane = await this.userManager.FindByEmailAsync("jane@doe.com");
        var bob = await this.userManager.FindByEmailAsync("bob@smith.com");
        var now = DateTime.Now;
        var posts = new List<Post>
        {
          new Post {
            ApplicationUser = john,
            Title = "John Post 1",
            Body = "This is John Post 1 Body content",
            CreatedAt = now
          },
          new Post {
            ApplicationUser = john,
            Title = "John Post 2",
            Body = "This is John Post 2 Body Content",
            CreatedAt = now
          },
          new Post {
            ApplicationUser = jane,
            Title = "Post 1 From Jane",
            Body = "Jane 1 Body content",
            CreatedAt = now
          },
          new Post {
            ApplicationUser = jane,
            Title = "Jane 2",
            Body = "This is Jane 2 Post Body Content Here.",
            CreatedAt = now
          },
          new Post {
            ApplicationUser = bob,
            Title = "Bob 1",
            Body = "This is Bob 1 Post Body",
            CreatedAt = now
          },
          new Post {
            ApplicationUser = bob,
            Title = "Bob Post 2",
            Body = "This is Bob Post 2 Body of content",
            CreatedAt = now
          }
        };
        await this.dbContext.AddRangeAsync(posts);
        this.dbContext.SaveChanges();
      }
      return Ok("Database Seeded");
    }
  }
}
