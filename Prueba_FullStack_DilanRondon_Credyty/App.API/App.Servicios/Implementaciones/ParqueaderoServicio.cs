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
    public class ParqueaderoServicio : IParqueaderoServicio
    {
        private readonly IParqueaderoRepositorio _parqueaderoRepositorio;
        private readonly IMapper _mapper;

        public ParqueaderoServicio(IParqueaderoRepositorio parqueaderoRepositorio, IMapper mapper)
        {
            _parqueaderoRepositorio = parqueaderoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ParqueaderoDTO>> ObtenerIngresos()
        {
            var entidades = await _parqueaderoRepositorio.ConsultarIngresosPorID();
            return _mapper.Map<List<ParqueaderoDTO>>(entidades);
        }
        public async Task<ParqueaderoDTO> ObtenerIngresoPorId(int? id = null)
        {
            var entidad = await _parqueaderoRepositorio.ConsultarIngresosPorID(id);
            return _mapper.Map<ParqueaderoDTO>(entidad.FirstOrDefault());
        }
        public async Task<bool> RegistrarIngreso(ParqueaderoDTO parqueaderoDto)
        {
            var entidad = _mapper.Map<ParqueaderoEntidad>(parqueaderoDto);
            return await _parqueaderoRepositorio.RegistrarIngreso(entidad);
        }
        public async Task<bool> ModificarIngreso(ParqueaderoDTO parqueaderoDto, int? id = null)
        {
            var ingreso = await ObtenerIngresoPorId(id);
            if (ingreso == null)
                throw new Exception("No se encontro el Ingreso");
            var entidad = _mapper.Map<ParqueaderoEntidad>(parqueaderoDto);
            return await _parqueaderoRepositorio.ModificarIngreso(entidad);
        }
        public async Task<bool> EliminarIngreso(int id)
        {
            return await _parqueaderoRepositorio.EliminarIngreso(id);
        }
    }
}
