using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.DTOs
{
    public class FacturaDTO
    {
        public int FacturaID { get; set; }
        public int IngresoID { get; set; }
        public string? NumeroFactura { get; set; }
    }
}
