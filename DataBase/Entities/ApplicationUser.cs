using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SharePosts.DataBase.Entities
{
  public class ApplicationUser : IdentityUser
  {
    [NotMapped]
    public string Password { get; set; }

    [NotMapped]
    public string AuthenticationToken { get; set; } = null;
  }
}
