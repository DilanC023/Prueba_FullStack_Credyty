using App.Repositorio.Entidades;
using App.Repositorio.Interfaces;
using App.Servicios.DTOs;
using App.Servicios.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.Implementaciones
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<UsuarioDTO> ObtenerUsuarioPorId(int? usuarioId = null)
        {
            var usuario = await _usuarioRepositorio.ConsultarUsuarioPorID(usuarioId);
            return _mapper.Map<UsuarioDTO>(usuario.FirstOrDefault());
        }
        public async Task<List<UsuarioDTO>> ObtenerUsuarios()
        {
            var usuarios = await _usuarioRepositorio.ConsultarUsuarioPorID();
            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }
        public async Task<bool> AdicionarUsuario(UsuarioDTO usuarioDto)
        {
            var entidad = _mapper.Map<UsuarioEntidad>(usuarioDto);
            return await _usuarioRepositorio.AdicionarUsuario(entidad);
        }
        public async Task<bool> ModificarUsuario(int id, UsuarioDTO usuarioDto)
        {
            var usuario = await ObtenerUsuarioPorId(id);
            if (usuario == null)
                throw new Exception("No se encontro el Usuario");
            var entidad = _mapper.Map<UsuarioEntidad>(usuarioDto);
            return await _usuarioRepositorio.ModificarUsuario(entidad);
        }
        public async Task<bool> EliminarUsuario(int id)
        {
            return await _usuarioRepositorio.EliminarUsuario(id);
        }
    }
}
