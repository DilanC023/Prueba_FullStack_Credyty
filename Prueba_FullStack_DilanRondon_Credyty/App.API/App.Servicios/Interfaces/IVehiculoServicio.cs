using App.Servicios.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.Interfaces
{
    public interface IVehiculoServicio
    {
        Task<VehiculoDTO> ObtenerVehiculoPorId(int? vehiculoId = null);
        Task<List<VehiculoDTO>> ObtenerVehiculos();
        Task<VehiculoDTO> ObtenerVehiculoPorPlaca(string Placa);
        Task<VehiculoDTO> AdicionarVehiculo(VehiculoDTO vehiculoDto);
        Task<bool> ModificarVehiculo(VehiculoDTO vehiculoDto, int? id = null);
        Task<bool> EliminarVehiculo(int id);

    }
}
