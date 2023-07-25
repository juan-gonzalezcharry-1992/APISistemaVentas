using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVenta.Model;

namespace SistemaVenta.DAL.Repositorios.contrato
{
    public interface IventaRepository: IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta modelo);
    }
}
