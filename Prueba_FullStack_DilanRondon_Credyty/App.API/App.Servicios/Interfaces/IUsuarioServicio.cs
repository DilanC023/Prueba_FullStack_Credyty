using App.Servicios.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.Interfaces
{
    public interface IUsuarioServicio
    {
        Task<UsuarioDTO> ObtenerUsuarioPorId(int? usuarioId = null);
        Task<List<UsuarioDTO>> ObtenerUsuarios();
        Task<bool> AdicionarUsuario(UsuarioDTO usuarioDto);
        Task<bool> ModificarUsuario(int id, UsuarioDTO usuarioDto);
        Task<bool> EliminarUsuario(int id);

    }
}
