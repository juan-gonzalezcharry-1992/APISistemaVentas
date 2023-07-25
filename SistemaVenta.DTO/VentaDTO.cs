﻿using SistemaVenta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? TipoPago { get; set; }

        public string? TextoTotal { get; set; }

        public string? FechaRegistro { get; set; }

        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
