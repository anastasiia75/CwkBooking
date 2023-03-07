using CwkBooking.DAL;
using CwkBooking.DAL.Repositories;
using CwkBooking.Services.Services;
using CwkBooling.Domain.Abstractions.Repositories;
using CwkBooling.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CwkBooking.Api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CwkBooking.Api", Version = "v1" });
            });

            services.AddHttpContextAccessor();

            var cs = Configuration.GetConnectionString("Default");
            services.AddDbContext<DataContext>(options => { options.UseSqlServer(cs); });
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IHotelsRepository, HotelRepository>();
            services.AddScoped<IReservationService, ReservationService>();

            // will create an instance the first time it is required
            // afterwards it always passes the exact same instance to all consumers

            //will create an instance each time it is required
            //services.AddTransient();

            //it creates and instance for each request
            //services.AddScoped();

            //services.AddSingleton<ISingletonOperation, SingletonOperation>();
            // services.AddTransient<ITransientOperation, TransientOperation>();
            //services.AddScoped<IScopedOperation, ScopedOperation>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CwkBooking.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // app.UseDateTimeHeader();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
