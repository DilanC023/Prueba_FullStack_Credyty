using App.Repositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<bool> AdicionarUsuario(UsuarioEntidad usuario);
        Task<bool> ModificarUsuario(UsuarioEntidad usuario);
        Task<bool> EliminarUsuario(int usuarioID);
        Task<List<UsuarioEntidad>> ConsultarUsuarioPorID(int? usuarioID = null);
    }
}
