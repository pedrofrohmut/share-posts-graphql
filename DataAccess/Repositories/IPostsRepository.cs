using System.Collections.Generic;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories
{
  public interface IPostsRepository
  {
    /*
     * Query: Get All Posts
     */
    IEnumerable<Post> GetAll();

    /*
     * Query: Get one post, if found, with the passed id
     */
    Post FindById(string id);

    /*
     * Query: Creates a new Post
     */
    void Create(Post newPost);

    /*
     * Mutation: Update the target post of the passed id with the updatedPost body
     */
    void Update(string id, Post updatedPost);

    /*
     * Mutation: Delete an post with the passed id
     */
    void Delete(string id);
  }
}
