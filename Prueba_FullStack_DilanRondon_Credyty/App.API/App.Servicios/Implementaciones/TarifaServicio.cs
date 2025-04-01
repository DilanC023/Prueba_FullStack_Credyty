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
    public class TarifaServicio : ITarifaServicio
    {
        private readonly ITarifaRepositorio _tarifaRepositorio;
        private readonly IMapper _mapper;

        public TarifaServicio(ITarifaRepositorio tarifaRepositorio, IMapper mapper)
        {
            _tarifaRepositorio = tarifaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<TarifaDTO>> ObtenerTarifas()
        {
            var entidades = await _tarifaRepositorio.ConsultarPorIDTarifa();
            return _mapper.Map<List<TarifaDTO>>(entidades);
        }
        public async Task<TarifaDTO> ObtenerTarifaPorId(int? id)
        {
            var entidad = await _tarifaRepositorio.ConsultarPorIDTarifa(id);
            return _mapper.Map<TarifaDTO>(entidad.FirstOrDefault());
        }
        public async Task<bool> AdicionarTarifa(TarifaDTO tarifaDto)
        {
            var entidad = _mapper.Map<TarifaEntidad>(tarifaDto);
            return await _tarifaRepositorio.AdicionarTarifa(entidad);
        }
        public async Task<bool> ModificarTarifa(TarifaDTO tarifaDto, int? id = null)
        {
            var existeTarifa = await ObtenerTarifaPorId(id);
            if (existeTarifa == null)
                throw new Exception("No se encontro la Tarifa");
            var entidad = _mapper.Map<TarifaEntidad>(tarifaDto);
            return await _tarifaRepositorio.ModificarTarifa(entidad);
        }
        public async Task<bool> EliminarTarifa(int id)
        {
            return await _tarifaRepositorio.EliminarTarifa(id);
        }
    }
}
