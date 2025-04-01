using App.Repositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.Interfaces
{
    public interface IVehiculoRepositorio
    {
        Task<int> AdicionarVehiculo(VehiculoEntidad vehiculo);
        Task<bool> ModificarVehiculo(VehiculoEntidad vehiculo);
        Task<bool> EliminarVehiculo(int vehiculoID);
        Task<List<VehiculoEntidad>> ConsultarVehiculoPorPlaca(string placa);
        Task<List<VehiculoEntidad>> ConsultarVehiculoPorID(int? vehiculoID = null);

    }
}
