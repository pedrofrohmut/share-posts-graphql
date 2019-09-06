using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharePosts.DataBase.Context;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories.Implementations
{
  public class ApplicationUsersRepository : IApplicationUsersRepository
  {
    private readonly SharePostsDbContext context;
    private readonly UserManager<ApplicationUser> userManager;

    public ApplicationUsersRepository(
        SharePostsDbContext context,
        UserManager<ApplicationUser> userManager)
    {
      this.context = context;
      this.userManager = userManager;
    }

    public async Task<ApplicationUser> Create(ApplicationUser newUser, string password)
    {
      if (String.IsNullOrWhiteSpace(newUser.UserName))
      {
        newUser.UserName = newUser.Email;
      }
      await this.userManager.CreateAsync(newUser, password);
      return newUser;
    }

    public async Task<ApplicationUser> FindByEmail(string email) =>
      await this.userManager.FindByEmailAsync(email);

    public async Task<ApplicationUser> FindById(string id) =>
      await this.userManager.FindByIdAsync(id);

    public async Task<IEnumerable<ApplicationUser>> GetAll() =>
      await this.context.ApplicationUsers.ToListAsync();

    public async Task<string> GetAuthenticationToken(string email, string password)
    {
      throw new System.NotImplementedException();
    }
  }
}
