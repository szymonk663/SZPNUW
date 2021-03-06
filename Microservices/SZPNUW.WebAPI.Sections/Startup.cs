﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using SZPNUW.Base.Consts;
using SZPNUW.DBService;

namespace SZPNUW.WebAPI.Sections
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DBService.Model.SZPNUWContext.ConnectionString = this.Configuration.GetValue<string>("CONNECTION_STRING");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(Consts.AuthenticateScheme)
                .AddCookie(Consts.AuthenticateScheme, opts =>
                {
                    opts.LoginPath = "/Account/LogIn";
                    opts.LogoutPath = "/Account/LogOut";
                    opts.AccessDeniedPath = "/Account/UnAuthenticated";
                    opts.Cookie.Name = "SZPNUW.Authentication";
                    opts.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                    opts.Cookie.SameSite = SameSiteMode.None;
                });
            services.AddSession(opts =>
            {
                opts.Cookie.Name = "SZPNUW.Session";
                opts.IdleTimeout = TimeSpan.FromHours(6);
                opts.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opts.Cookie.SameSite = SameSiteMode.None;
            });
            services
                .AddMvc()
                .AddJsonOptions(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Sections", new Info { Title = "Sections", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandling();
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }
            //app.UseExceptionHandler();
            app.UseSession();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/Sections/swagger.json", "Sections");
                c.RoutePrefix = "Sections/swagger";
            });
            app.UseMvc();
        }
    }
}
