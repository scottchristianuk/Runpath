using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Runpath.Media.Net;
using Runpath.Media.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Runpath.Media.Api
{
    public class Startup
    {
        #region Properties

        public string PhotoAlbumApiClientUri => Configuration.GetValue<string>(nameof(PhotoAlbumApiClientUri));

        public IConfiguration Configuration { get; }

        #endregion

        #region Ctor.

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHttpClient<IApiClient, JsonApiClient>(configure =>
            {
                configure.BaseAddress = new Uri(PhotoAlbumApiClientUri);
            });

            services.AddTransient<IPhotoAlbumService, PhotoAlbumService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Runpath PhotoAlbum API", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // add Swagger for convenience
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Runpath PhotoAlbum API"); });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}