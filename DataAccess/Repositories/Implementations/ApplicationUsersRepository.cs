using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharePosts.DataBase.Context;
using SharePosts.DataBase.Entities;

namespace SharePosts.DataAccess.Repositories.Implementations
{
  public class ApplicationUsersRepository : IApplicationUsersRepository
  {
    private readonly SharePostsDbContext context;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IConfiguration config;

    public ApplicationUsersRepository(
        SharePostsDbContext context,
        UserManager<ApplicationUser> userManager,
        IConfiguration config)
    {
      this.context = context;
      this.userManager = userManager;
      this.config = config;
    }

    public async Task<ApplicationUser> Create(ApplicationUser newUser, string password)
    {
      if (String.IsNullOrWhiteSpace(newUser.UserName))
        newUser.UserName = newUser.Email;
      await this.userManager.CreateAsync(newUser, password);
      return newUser;
    }

    public async Task<ApplicationUser> Create(string userName, string email, string password)
    {
      if (String.IsNullOrWhiteSpace(userName))
        userName = email;
      var newUser = new ApplicationUser { UserName = userName, Email = email };
      await this.userManager.CreateAsync(newUser, password);
      return newUser;
    }

    public async Task<ApplicationUser> FindByEmail(string email) =>
      await this.userManager.FindByEmailAsync(email);

    public async Task<ApplicationUser> FindById(string id) =>
      await this.userManager.FindByIdAsync(id);

    public async Task<IEnumerable<ApplicationUser>> GetAll() =>
      await this.context.ApplicationUsers.ToListAsync();

    public async Task<ApplicationUser> GetAuthenticatedUser(string email, string password)
    {
      var authenticatedUser = await this.userManager.FindByEmailAsync(email);
      if (authenticatedUser == null) 
        return null;
      var validPassword = await this.userManager.CheckPasswordAsync(authenticatedUser, password);
      if (!validPassword) 
        return null;
      authenticatedUser.AuthenticationToken = 
        GenerateAuthenticationToken(authenticatedUser.Id, email, authenticatedUser.EmailConfirmed);
      return authenticatedUser;
    }

    private string GenerateAuthenticationToken(string userId, string userEmail, bool isEmailConfirmed)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["JWT_SECRET"].ToString()));
      var securityAlgorithm = SecurityAlgorithms.HmacSha256Signature;
      var options = new IdentityOptions();
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim("id", userId),
          new Claim("email", userEmail),
          new Claim("isEmailConfirmed", isEmailConfirmed.ToString())
        }),
        Expires = DateTime.UtcNow.AddDays(1),
        SigningCredentials = new SigningCredentials(securityKey, securityAlgorithm)
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var securityToken = tokenHandler.CreateToken(tokenDescriptor);
      var token = tokenHandler.WriteToken(securityToken);
      return token;
    }
  }
}
