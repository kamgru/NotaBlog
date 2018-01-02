using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using NotaBlog.Admin.Data;
using NotaBlog.Admin.Models;
using NotaBlog.Admin.Services;
using NotaBlog.Api.Services;
using NotaBlog.Core.Commands;
using NotaBlog.Core.Repositories;
using NotaBlog.Core.Services;
using NotaBlog.Persistence;

namespace NotaBlog.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var storyRepository = new StoryRepository(new MongoClient("mongodb://localhost:27017").GetDatabase("NotaBlog"));

            services.AddTransient<IStoryRepository>(x => storyRepository);
            services.AddTransient<ISettingsRepository>(
                factory => new SettingsRepository(new MongoClient("mongodb://localhost:27017").GetDatabase("NotaBlog")));
            services.AddTransient<StoryAdminService>();

            var commandDispatcher = new CommandDispatcher();
            commandDispatcher.RegisterHandler(new CreateStoryHandler(storyRepository, new DateTimeProvider()));
            commandDispatcher.RegisterHandler(new PublishStoryHandler(storyRepository, new DateTimeProvider()));
            commandDispatcher.RegisterHandler(new UpdateStoryHandler(storyRepository, new DateTimeProvider()));
            commandDispatcher.RegisterHandler(new UnpublishStoryHandler(storyRepository));
            commandDispatcher.RegisterHandler(new SetSetNameCommandHandler(storyRepository));
            services.AddTransient<ICommandDispatcher>(x => commandDispatcher);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Users")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>(cfg => 
            {
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredLength = 8;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            
            services.AddTransient<ConfigurationService>();
            services.AddMvc();

            ConfigureJwt(services);
        }

        private void ConfigureJwt(IServiceCollection services)
        {
            var tokenConfiguration = Configuration.GetSection("TokenConfiguration")
                .Get<TokenConfiguration>();

            services.AddAuthentication().AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = !HostingEnvironment.IsDevelopment();
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = tokenConfiguration.Issuer,
                    ValidAudience = tokenConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddTransient<ISecurityTokenValidator, JwtSecurityTokenHandler>(factory => {
                var handler = new JwtSecurityTokenHandler();
                handler.InboundClaimTypeMap.Clear();
                return handler;
            });
            services.AddTransient<IAccessTokenFactory, AccessTokenFactory >();
            services.AddTransient<LoginService>();
            services.AddTransient<RenewAccessService>();
            services.AddTransient(x => tokenConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            HostingEnvironment = env;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
