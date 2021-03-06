using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using diplomski_backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using diplomski_backend.Services.CategoryService;
using Microsoft.AspNetCore.Http;
using diplomski_backend.Services.ProductService;
using diplomski_backend.Services.OrderService;
using Microsoft.AspNetCore.Cors.Infrastructure;
using diplomski_backend.Services.Settings;
using diplomski_backend.Services.MailService;

namespace diplomski_backend
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
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IMailService, MailService>();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            }
            );
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:8080", "http://localhost:8080/login")
               .AllowAnyMethod()
               .AllowAnyHeader();
    }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICorsService corsService, ICorsPolicyProvider corsPolicyProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                OnPrepareResponse = async (ctx) =>
                {
                    CorsPolicy policy = await corsPolicyProvider.GetPolicyAsync(ctx.Context, "MyPolicy");

                    CorsResult result = corsService.EvaluatePolicy(ctx.Context, policy);

                    corsService.ApplyResult(result, ctx.Context.Response);
                }
            });

            app.UseCors("MyPolicy");
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // app.UseCors("MyPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
