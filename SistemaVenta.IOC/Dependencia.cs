using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVenta.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.DAL.Repositorios.contrato;
using SistemaVenta.DAL.Repositorios;
using SistemaVenta.utility;
namespace SistemaVenta.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<DbventacliContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IventaRepository,VentaRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}
