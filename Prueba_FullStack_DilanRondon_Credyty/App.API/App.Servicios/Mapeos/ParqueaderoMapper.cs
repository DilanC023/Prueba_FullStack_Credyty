using App.Repositorio.Entidades;
using App.Servicios.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Servicios.Mapeos
{
    public class ParqueaderoMapper : Profile
    {
        public ParqueaderoMapper()
        {
            CreateMap<ParqueaderoEntidad, ParqueaderoDTO>().ReverseMap();
        }
        public static ParqueaderoDTO ADto(ParqueaderoEntidad usuario, IMapper mapper)
        {
            return mapper.Map<ParqueaderoDTO>(usuario);
        }
        public static List<ParqueaderoEntidad> AEntidad(List<ParqueaderoDTO> usuarios, IMapper mapper)
        {
            return mapper.Map<List<ParqueaderoEntidad>>(usuarios);
        }
        public static List<ParqueaderoDTO> ADto(List<ParqueaderoEntidad> usuario, IMapper mapper)
        {
            return mapper.Map<List<ParqueaderoDTO>>(usuario);
        }
        public static ParqueaderoEntidad AEntidad(ParqueaderoDTO usuarios, IMapper mapper)
        {
            return mapper.Map<ParqueaderoEntidad>(usuarios);
        }
    }
}
