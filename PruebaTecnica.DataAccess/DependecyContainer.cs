using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.DataAccess
{
    public static class DependecyContainer
    {
        public static IServiceCollection AddDALDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            // Registrar la cadena de conexión como un servicio singleton
            string connectionString = configuration.GetConnectionString("url");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Registrar UserDAL con la cadena de conexión proporcionada
            services.AddScoped<UserDAL>(provider => new UserDAL(connectionString));

            return services;
        }
    }
}
