using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SharePosts.DataBase.Context;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories.Implementations
{
  public class PostsRepository : IPostsRepository
  {
    private readonly SharePostsDbContext context;

    public PostsRepository(SharePostsDbContext context)
    {
      this.context = context;
    }

    public async Task<Post> Create(Post newPost)
    {
      await this.context.Posts.AddAsync(newPost);
      await this.context.SaveChangesAsync();
      return newPost;
    }

    public async Task<Post> Delete(string id)
    {
      var postDb = await this.context.Posts
        .FirstOrDefaultAsync(post => post.Id == id);
      if (postDb != null) 
      {
        this.context.Remove(postDb);
        await this.context.SaveChangesAsync();
      }
      return postDb;
    }

    public async Task<Post> FindById(string id) =>
      await this.context.Posts
        .FirstOrDefaultAsync(post => post.Id == id);

    public async Task<IEnumerable<Post>> GetAll() =>
      await this.context.Posts
        .OrderBy(post => post.CreatedAt)
        .ToListAsync();

    public async Task<Post> Update(Post updatedPost)
    {
      var postDb = await this.context.Posts
        .FirstOrDefaultAsync(post => post.Id == updatedPost.Id);
      if (postDb != null) 
      {
        postDb.Title = updatedPost.Title;
        postDb.Body = updatedPost.Body;
        this.context.Posts.Update(postDb);
        await this.context.SaveChangesAsync();
      }
      return updatedPost;
    }
  }
}
