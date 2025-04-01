using App.Servicios.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.Interfaces
{
    public interface IFacturaServicio
    {
        Task<List<FacturaDTO>> ObtenerFacturas();
        Task<FacturaDTO> ObtenerFacturaPorId(int id);
        Task<bool> AdicionarFactura(FacturaDTO facturaDto);

    }
}
