using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.Entidades
{
    public class TarifaEntidad
    {
        public int TarifaID { get; set; }
        public string? TipoVehiculo { get; set; }
        public decimal PrecioPorMinuto { get; set; }
    }
}
