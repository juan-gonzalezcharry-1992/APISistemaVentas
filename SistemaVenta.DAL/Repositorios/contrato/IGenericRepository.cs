﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios.contrato
{
    public interface IGenericRepository<TModelo>where TModelo : class
    {
        Task<TModelo>Obtener(Expression<Func<TModelo, bool>> filtro);
        Task<TModelo> Crear(TModelo modelo);
        Task<bool> Editar(TModelo modelo);
        Task<bool> Eliminar(TModelo modelo);
        Task<IQueryable<TModelo>> Consultar(Expression<Func<TModelo,bool>> filtro = null);
    }
}
