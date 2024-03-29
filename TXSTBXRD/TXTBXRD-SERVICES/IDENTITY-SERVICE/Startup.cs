using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IDENTITY_SERVICE.Services;
using TXSTBXRD_LIBS.Security;


namespace IDENTITY_SERVICE
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
            services.AddMemoryCache();
            services.AddControllers();
            services.AddSingleton<IdentityService>();
            services.AddSingleton<CriptoSevice>();
            services.AddSingleton<UsersDAO>();
            services.AddTransient<UsersDbContext>(_ => new UsersDbContext(Configuration["ConnectionStrings:Default"]));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(options => options.WithOrigins("http://localhost:5117").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCors(options => options.WithOrigins("https://localhost:7114").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseCors(options => options.WithOrigins("http://localhost:5224").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseCors(options => options.WithOrigins("https://localhost:7077").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
