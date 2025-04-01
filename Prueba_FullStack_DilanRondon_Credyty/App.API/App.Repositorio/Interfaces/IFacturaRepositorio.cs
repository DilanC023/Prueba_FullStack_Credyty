using App.Repositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.Interfaces
{
    public interface IFacturaRepositorio
    {
        Task<bool> AdicionarFactura(int ingresoID, string numeroFactura);
        Task<List<FacturaEntidad>> ConsultarFacturas(int? facturaID = null);
    }
}
