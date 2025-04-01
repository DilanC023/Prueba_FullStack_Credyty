using App.Repositorio.Entidades;
using App.Servicios.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace App.Servicios.Mapeos
{
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<UsuarioEntidad, UsuarioDTO>().ReverseMap();
        }
        public static UsuarioDTO ADto(UsuarioEntidad usuario, IMapper mapper)
        {
            return mapper.Map<UsuarioDTO>(usuario);
        }
        public static List<UsuarioEntidad> AEntidad(List<UsuarioDTO> usuarios, IMapper mapper)
        {
            return mapper.Map<List<UsuarioEntidad>>(usuarios);
        }
        public static List<UsuarioDTO> ADto(List<UsuarioEntidad> usuario, IMapper mapper)
        {
            return mapper.Map<List<UsuarioDTO>>(usuario);
        }
        public static UsuarioEntidad AEntidad(UsuarioDTO usuarios, IMapper mapper)
        {
            return mapper.Map<UsuarioEntidad>(usuarios);
        }
    }
}
