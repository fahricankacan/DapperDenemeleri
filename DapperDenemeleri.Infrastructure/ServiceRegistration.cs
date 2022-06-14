using DapperDenemeleri.Application.Interfaces;
using DapperDenemeleri.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDenemeleri.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped((s) => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetService<SqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });
        }
    }
}
