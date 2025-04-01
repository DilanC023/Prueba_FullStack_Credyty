using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.DTOs
{
    public class VehiculoDTO
    {
        public int VehiculoID { get; set; }
        public string? TipoVehiculo { get; set; }
        public string? Placa { get; set; }
        public string? DocumentoIdentidad { get; set; }
    }
}
