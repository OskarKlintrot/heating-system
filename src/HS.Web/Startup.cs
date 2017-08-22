using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using HS.Web.Models.Options;
using HS.Web.Extensions;
using Microsoft.AspNetCore.ResponseCompression;

namespace HS.Web
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
            #region Database
            var sqlConnectionString = Configuration["CONNECTION_STRING"];

            //services.AddDbContext<IntranetApiContext>(opt => opt.UseSqlServer(sqlConnectionString));
            #endregion

            #region Authentication
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddAzureAd(options =>
            {
                options.CallbackPath = Configuration["AUTHENTICATION_AZURE_AD_CALLBACK_PATH"];
                options.ClientId = Configuration["AUTHENTICATION_AZURE_AD_CLIENT_ID"];
                options.Instance = Configuration["AUTHENTICATION_AZURE_AD_AADINSTANCE"];
                options.TenantId = Configuration["AUTHENTICATION_AZURE_AD_TENANT_ID"];
            })
            .AddCookie();

            ClaimsPrincipalExtension.AdminGroupId = Configuration["AUTHENTICATION_AZURE_AD_ADMIN_GROUP"];
            #endregion

            #region Custom Authorization Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "IsAdmin",
                    policyBuilder => policyBuilder.RequireAssertion(
                        context => context.User.IsAdmin())
                    );
            });
            #endregion

            #region Options
            services.Configure<GoogleAnalyticsOptions>(options =>
            {
                options.TrackingId = Configuration["GA_TRACKING_ID"];
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
                options.Level = System.IO.Compression.CompressionLevel.Optimal
            );
            #endregion

            // TODO: Enforce SSL: https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl

            #region Response Compression
            services.AddResponseCompression();
            #endregion

            #region MVC
            services.AddMvc();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            #region Error Pages
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            #endregion

            #region Response Compression
            app.UseResponseCompression();
            #endregion

            #region Authentication
            app.UseAuthentication(); 
            #endregion

            #region Static Files
            app.UseProtectFolder(new ProtectFolderOptions
            {
                Path = "/Assets",
            });

            app.UseStaticFiles(); // For the wwwroot folder

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Assets")
                ),
                RequestPath = new PathString("/Assets")
            });
            #endregion

            #region MVC
            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");
                }); 
            #endregion
        }
    }
}
