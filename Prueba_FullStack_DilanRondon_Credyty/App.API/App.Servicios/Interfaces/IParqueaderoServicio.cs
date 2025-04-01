using App.Servicios.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.Interfaces
{
    public interface IParqueaderoServicio
    {
        Task<List<ParqueaderoDTO>> ObtenerIngresos();
        Task<ParqueaderoDTO> ObtenerIngresoPorId(int? id = null);
        Task<bool> RegistrarIngreso(ParqueaderoDTO parqueaderoDto);
        Task<bool> ModificarIngreso(ParqueaderoDTO parqueaderoDto, int? id = null);
        Task<bool> EliminarIngreso(int id);

    }
}
