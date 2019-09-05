using System.Collections.Generic;
using System.Threading.Tasks;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories
{
  public interface IPostsRepository
  {
    /*
     * Query: Get All Posts
     */
    Task<IEnumerable<Post>> GetAll();

    /*
     * Query: Get one post, if found, with the passed id
     */
    Task<Post> FindById(string id);

    /*
     * Query: Creates a new Post
     */
    Task Create(Post newPost);

    /*
     * Mutation: Update the target post of the passed id with the updatedPost body
     */
    Task Update(string id, Post updatedPost);

    /*
     * Mutation: Delete an post with the passed id
     */
    Task Delete(string id);
  }
}
