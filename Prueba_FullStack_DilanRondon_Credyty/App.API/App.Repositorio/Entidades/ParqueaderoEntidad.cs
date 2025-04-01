using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.Entidades
{
    public class ParqueaderoEntidad
    {
        public int IngresoID { get; set; }
        public int VehiculoID { get; set; }
        public DateTime? HoraIngreso { get; set; }
        public DateTime? HoraSalida { get; set; }
        public decimal? ValorPagado { get; set; }
        public bool? DescuentoAplicado { get; set; }
        public string? TipoVehiculo { get; set; } 
        public string? Placa { get; set; } 
        public string? DocumentoIdentidad { get; set; } 
    }
}
