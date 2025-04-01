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
    public class TarifaMapper : Profile
    {
        public TarifaMapper()
        {
            CreateMap<TarifaEntidad, TarifaDTO>().ReverseMap();
        }
        public static TarifaDTO ADto(TarifaEntidad usuario, IMapper mapper)
        {
            return mapper.Map<TarifaDTO>(usuario);
        }
        public static List<TarifaEntidad> AEntidad(List<TarifaDTO> usuarios, IMapper mapper)
        {
            return mapper.Map<List<TarifaEntidad>>(usuarios);
        }
        public static List<TarifaDTO> ADto(List<TarifaEntidad> usuario, IMapper mapper)
        {
            return mapper.Map<List<TarifaDTO>>(usuario);
        }
        public static TarifaEntidad AEntidad(TarifaDTO usuarios, IMapper mapper)
        {
            return mapper.Map<TarifaEntidad>(usuarios);
        }
    }
}
