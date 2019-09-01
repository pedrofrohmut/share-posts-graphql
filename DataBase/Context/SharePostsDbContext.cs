using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataBase.Context
{
  public class SharePostsDbContext : IdentityDbContext
  {
    public SharePostsDbContext(DbContextOptions<SharePostsDbContext> options) : base (options) { }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Post> Posts { get; set; }
  }
}
