using AutoMapper;
using HbCampaignModule.API.MapperProfile;
using HbCampaignModule.Domain.Interfaces;
using HbCampaignModule.Domain.Interfaces.DataIF;
using HbCampaignModule.Domain.Interfaces.ModelIF;
using HbCampaignModule.Infrastructure.Context;
using HbCampaignModule.Infrastructure.Exceptions;
using HbCampaignModule.Infrastructure.Repository.DataRepos;
using HbCampaignModule.Infrastructure.Repository.ModelRepos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HbCampaignModule
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
            services
            .AddDbContext<PostgreSqlDbContext>(options =>
            options.UseNpgsql("Host = 127.0.0.1; Database = CampaignModule; Username = postgres; Password = Zeynep1996?"));
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HbCampaignModule", Version = "v1" });
            //});
            services.AddControllers();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICampaignRepository, CampaignRepository>();
            services.AddTransient<IDataRepository, DataRepository>();
            services.AddTransient<ICampaignRepository, CampaignRepository>();
            services.AddAutoMapper(typeof(Startup));
            //Automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new API.MapperProfile.Mapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
            //});
            app.UseExceptionMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
