using App.Repositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.Interfaces
{
    public interface ITarifaRepositorio
    {
        Task<bool> AdicionarTarifa(TarifaEntidad tarifa);
        Task<bool> ModificarTarifa(TarifaEntidad tarifa);
        Task<bool> EliminarTarifa(int tarifaID);
        Task<List<TarifaEntidad>> ConsultarPorIDTarifa(int? tarifaID = null);
    }
}
