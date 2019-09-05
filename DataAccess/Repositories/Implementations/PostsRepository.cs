using System.Collections.Generic;
using System.Threading.Tasks;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories.Implementations
{
  public class PostsRepository : IPostsRepository
  {
    public Task Create(Post newPost)
    {
      throw new System.NotImplementedException();
    }

    public Task Delete(string id)
    {
      throw new System.NotImplementedException();
    }

    public Task<Post> FindById(string id)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Post>> GetAll()
    {
      throw new System.NotImplementedException();
    }

    public Task Update(string id, Post updatedPost)
    {
      throw new System.NotImplementedException();
    }
  }
}
