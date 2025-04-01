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
    public class VehiculoMapper : Profile
    {
        public VehiculoMapper()
        {
            // Mapeo de entidad a DTO
            CreateMap<VehiculoEntidad, VehiculoDTO>().ReverseMap();
        }

        public static VehiculoDTO ADto(VehiculoEntidad vehiculo, IMapper mapper)
        {
            return mapper.Map<VehiculoDTO>(vehiculo);
        }
        public static List<VehiculoEntidad> AEntidad(List<VehiculoDTO> vehiculos, IMapper mapper)
        {
            return mapper.Map<List<VehiculoEntidad>>(vehiculos);
        }
        public static List<VehiculoDTO> ADto(List<VehiculoEntidad> vehiculo, IMapper mapper)
        {
            return mapper.Map<List<VehiculoDTO>>(vehiculo);
        }
        public static VehiculoEntidad AEntidad(VehiculoDTO vehiculos, IMapper mapper)
        {
            return mapper.Map<VehiculoEntidad>(vehiculos);
        }
    }
}
