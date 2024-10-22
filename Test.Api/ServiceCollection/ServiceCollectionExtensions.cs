using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Test.Domain.Entities;
using Test.Domain.Interfaces;
using Test.Domain.Services;
using Test.Infrastructure.Persistence;
using Test.Infrastructure.Repository;

namespace Test.Api.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddDependencyInjection(this WebApplicationBuilder application)
        {
            var connectionString = application.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'SqlDbContext' not found.");
            application.Services.AddEntityFrameworkNpgsql()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            application.Services.AddScoped(typeof(IAsyncRepository<,>), typeof(AsyncRepository<,>));
            application.Services.AddTransient<IGenericService<CarBrands,Guid>,CarBrandsService>();
            application.Services.AddTransient<DataSeedService>();
           
            return application;
        }
    }
}
