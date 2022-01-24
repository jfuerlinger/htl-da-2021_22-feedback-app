using FeedbackApp.Core.Contracts.Persistence;
using FeedbackApp.Persistence;
using FeedbackApp.WebApi.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace FeedbackApp.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // For MariaDb (only for Test, DB changed to EF)
            //services.AddDbContext<MariaDbContext>(
            //dbContextOptions => dbContextOptions
            //    .UseMySql(Configuration.GetConnectionString("MariaDbConnectionString"), 
            //        ServerVersion.AutoDetect(Configuration.GetConnectionString("MariaDbConnectionString"))));

            //services.AddScoped<ITestDataService, TestDataService>();

            // For EF IdentyServer
            services.AddDbContext<ApplicationDbContext>
                (
                    options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr"))
                );

            // For EF FeedbackDB
            services.AddDbContext<FeedbackDbContext>
                (
                    options => options.UseSqlServer(Configuration.GetConnectionString("FeedbackDbConnectionString"))
                );
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    var allowed = options.User.AllowedUserNameCharacters + "‰¸ˆƒ‹÷";
                    options.User.AllowedUserNameCharacters = allowed;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FeedbackApp_WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FeedbackApp_WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
