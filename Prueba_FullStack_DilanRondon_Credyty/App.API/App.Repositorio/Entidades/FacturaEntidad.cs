using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.Entidades
{
    public class FacturaEntidad
    {
        public int FacturaID { get; set; }
        public int IngresoID { get; set; }
        public string? NumeroFactura { get; set; }
    }
}
