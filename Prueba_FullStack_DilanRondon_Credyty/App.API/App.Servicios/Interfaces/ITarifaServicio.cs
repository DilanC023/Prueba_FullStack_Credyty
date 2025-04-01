using App.Servicios.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.Interfaces
{
    public interface ITarifaServicio
    {
        Task<List<TarifaDTO>> ObtenerTarifas();
        Task<TarifaDTO> ObtenerTarifaPorId(int? id);
        Task<bool> AdicionarTarifa(TarifaDTO tarifaDto);
        Task<bool> ModificarTarifa(TarifaDTO tarifaDto, int? id = null);
        Task<bool> EliminarTarifa(int id);

    }
}
