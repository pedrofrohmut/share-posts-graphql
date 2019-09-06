using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharePosts.DataBase.Context;
using SharePosts.DataBase.Entities;

namespace SharePosts.WebApi.Controllers
{
  [Route("seed_database")]
  [ApiController]
  public class SeedController : ControllerBase
  {
    private readonly SharePostsDbContext context;
    private readonly UserManager<ApplicationUser> userManager;

    public SeedController(
        SharePostsDbContext context,
        UserManager<ApplicationUser> userManager)
    {
      this.context = context;
      this.userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult> Seed()
    {
      var password = "1234";
      if (!this.context.ApplicationUsers.Any())
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
            Author = john,
            Title = "John Post 1",
            Body = "This is John Post 1 Body content",
            CreatedAt = now
          },
          new Post {
            Author = john,
            Title = "John Post 2",
            Body = "This is John Post 2 Body Content",
            CreatedAt = now
          },
          new Post {
            Author = jane,
            Title = "Post 1 From Jane",
            Body = "Jane 1 Body content",
            CreatedAt = now
          },
          new Post {
            Author = jane,
            Title = "Jane 2",
            Body = "This is Jane 2 Post Body Content Here.",
            CreatedAt = now
          },
          new Post {
            Author = bob,
            Title = "Bob 1",
            Body = "This is Bob 1 Post Body",
            CreatedAt = now
          },
          new Post {
            Author = bob,
            Title = "Bob Post 2",
            Body = "This is Bob Post 2 Body of content",
            CreatedAt = now
          }
        };
        await this.context.AddRangeAsync(posts);
        this.context.SaveChanges();
        return Ok("Database Seeded");
      }
      else
      {
        return Ok("Data already seeded");
      }
    }
  }
}
