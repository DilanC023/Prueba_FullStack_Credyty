using App.Repositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.Interfaces
{
    public interface IParqueaderoRepositorio
    {
        Task<bool> RegistrarIngreso(ParqueaderoEntidad parqueadero);
        Task<bool> ModificarIngreso(ParqueaderoEntidad parqueadero);
        Task<bool> EliminarIngreso(int ingresoID);
        Task<List<ParqueaderoEntidad>> ConsultarIngresosPorID(int? ingresoID = null);


    }
}
