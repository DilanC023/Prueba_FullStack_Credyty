using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.DTOs
{
    public class TarifaDTO
    {
        public int TarifaID { get; set; }
        public string? TipoVehiculo { get; set; }
        public decimal PrecioPorMinuto { get; set; }
    }
}
