using System;
using System.Text;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharePosts.DataAccess.Repositories;
using SharePosts.DataAccess.Repositories.Implementations;
using SharePosts.DataBase.Context;
using SharePosts.DataBase.Entities;
using SharePosts.WebApi.Mutations;
using SharePosts.WebApi.Queries;
using SharePosts.WebApi.Schema;
using SharePosts.WebApi.Types;

namespace WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      // DbContext
      services.AddEntityFrameworkNpgsql()
        .AddDbContext<SharePostsDbContext>(options =>
            options.UseNpgsql(Configuration["ConnectionStrings:PostgreSQL:SharePostsDb"]));
      // Identity (userManager, signInManager ...)
      services.AddDefaultIdentity<ApplicationUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<SharePostsDbContext>();
      // Identity Options
      services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 4;
      });
      // Configuration
      services.AddSingleton<IConfiguration>(this.Configuration);
      // DocumentExecuter
      services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
      // Repositories
      services.AddTransient<IApplicationUsersRepository, ApplicationUsersRepository>();
      services.AddTransient<IPostsRepository, PostsRepository>();
      // Query
      services.AddSingleton<RootQuery>();
      // Mutations
      services.AddSingleton<RootMutation>();
      // Types
      services.AddSingleton<ApplicationUserType>();
      services.AddSingleton<ApplicationUserInputType>();
      services.AddSingleton<PostType>();
      services.AddSingleton<PostInputType>();
      var serviceProvider = services.BuildServiceProvider();
      // Schema
      services.AddSingleton<ISchema>(
          new SharePostsSchema(
            new FuncDependencyResolver(type => serviceProvider.GetService(type))));
      // Cors
      services.AddCors();
      // JWT Authentication
      var key = Encoding.UTF8.GetBytes(Configuration["JWT_SECRET"].ToString());
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false,
          ClockSkew = TimeSpan.Zero
        };
      });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();
      app.UseCors(builder =>
        builder.WithOrigins(Configuration["CLIENT_URL"]).AllowAnyHeader().AllowAnyOrigin());
      app.UseAuthentication();
      app.UseGraphiQl();
      app.UseMvc();
    }
  }
}
