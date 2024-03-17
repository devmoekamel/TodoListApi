
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using todlistApi.Model;
using TodolistApi.Context;
using TodolistApi.Helper;
using TodolistApi.Services.Auth;
using TodolistApi.Services.TaskkService;
using TodolistApi.Services.TaskService;

namespace TodolistApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
     //=========================================================
            //Register AppliactionDBContext 
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
            });
     //=========================================================
            //Identity User - Roles
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>();

            //===============Register Services===================================

            builder.Services.AddScoped<IAuthService,AuthService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            //===============Map JWT Vlues===================================
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
   
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtoptions =>
            {
                jwtoptions.RequireHttpsMetadata = false;
                jwtoptions.SaveToken = false;
                jwtoptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });



            //==============================================================
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
           
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
