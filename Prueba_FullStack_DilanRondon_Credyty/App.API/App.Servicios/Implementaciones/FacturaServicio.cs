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
    public class FacturaServicio : IFacturaServicio
    {
        private readonly IFacturaRepositorio _facturaRepositorio;
        private readonly IMapper _mapper;

        public FacturaServicio(IFacturaRepositorio facturaRepositorio, IMapper mapper)
        {
            _facturaRepositorio = facturaRepositorio;
            _mapper = mapper;
        }

        public async Task<List<FacturaDTO>> ObtenerFacturas()
        {
            var entidades = await _facturaRepositorio.ConsultarFacturas();
            return _mapper.Map<List<FacturaDTO>>(entidades);
        }
        public async Task<FacturaDTO> ObtenerFacturaPorId(int id)
        {
            var entidad = await _facturaRepositorio.ConsultarFacturas(id);
            return _mapper.Map<FacturaDTO>(entidad.FirstOrDefault());
        }
        public async Task<bool> AdicionarFactura(FacturaDTO facturaDto)
        {
            return await _facturaRepositorio.AdicionarFactura(facturaDto.IngresoID, facturaDto.NumeroFactura);
        }

    }
}
