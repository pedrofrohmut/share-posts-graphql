using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SharePosts.DataBase.DbContext;
using SharePosts.DataBase.Entities;

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
      // IdentityDbContext
      services.AddEntityFrameworkNpgsql()
        .AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(Configuration["ConnectionStrings:PostgreSQL:SharePostsDb"]));
      // Enable General Identity Services
      services.AddDefaultIdentity<ApplicationUser>()
          .AddRoles<IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>();
      // Removes Compplicate Passwords Requirements
      services.Configure<IdentityOptions>(options =>
     {
       options.Password.RequireDigit = false;
       options.Password.RequireUppercase = false;
       options.Password.RequireNonAlphanumeric = false;
       options.Password.RequireLowercase = false;
       options.Password.RequiredLength = 4;
     });
      // Cors
      services.AddCors();
      // JWT Authetication
      var key = Encoding.UTF8.GetBytes(Configuration["JWT_SECRET"].ToString());
      // Token Configuration
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
            ClockSkew = TimeSpan.Zero,
          };
        });
      // Configuration
      services.AddSingleton<IConfiguration>(Configuration);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseCors(builder =>
          builder.WithOrigins(Configuration["CLIENT_URL"])
            .AllowAnyHeader()
            .AllowAnyMethod());
      app.UseAuthentication();
      app.UseMvc();
    }
  }
}
