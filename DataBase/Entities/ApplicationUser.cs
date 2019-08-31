using System;
using Microsoft.AspNetCore.Identity;

namespace SharePosts.DataBase.Entities
{
  public class ApplicationUser : IdentityUser
    {
    public DateTime CreatedAt { get; set; }
  }
}
