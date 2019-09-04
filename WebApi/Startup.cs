using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharePosts.DataAccess.Repositories;
using SharePosts.DataAccess.Repositories.Implementations;
using SharePosts.DataBase.Context;
using SharePosts.DataBase.Entities;
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
      services.Configure<IdentityOptions>(options => {
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
      /* services.AddSingleton<ApplicationUsersQuery>(); */
      /* services.AddSingleton<PostsQuery>(); */
      // Mutations
      /* services.AddSingleton<RootMutation>(); */
      /* services.AddSingleton<ApplicationUserMutation>(); */
      /* services.AddSingleton<PostsMutation>(); */
      // Types
      services.AddSingleton<ApplicationUserType>();
      /* services.AddSingleton<ApplicationUserInputType>(); */
      /* services.AddSingleton<PostType>(); */
      /* services.AddSingleton<PostInputType>(); */
      var serviceProvider = services.BuildServiceProvider();
      // Schema
      services.AddSingleton<ISchema>(
          new SharePostsSchema(
            new FuncDependencyResolver(type => serviceProvider.GetService(type))));
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();
      app.UseAuthentication();
      app.UseGraphiQl();
      app.UseMvc();
    }
  }
}
