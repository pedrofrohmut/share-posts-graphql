using System;

namespace SharePosts.DataBase.Entities
{
  public class Post
  {
    public string Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }

    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
  }
}
