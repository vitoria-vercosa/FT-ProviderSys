using FT_ProviderSys.Configs;
using FT_ProviderSys.Services;
using FT_ProviderSys.Services.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using FT_ProviderSys.Data;
using FT_ProviderSys.Repositories.Interfaces;
using FT_ProviderSys.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FT_ProviderSys
{
    public class Startup
    {

        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigurationServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))) ; 
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });  

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProviderService, ProviderService>();
            
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();

            services.AddScoped<IValidationHelper, ValidationHelper>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment )
        {
            if (environment.IsDevelopment())
            {

            }
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseSwaggerUI();

            app.UseSwagger();
        }
    }
}
