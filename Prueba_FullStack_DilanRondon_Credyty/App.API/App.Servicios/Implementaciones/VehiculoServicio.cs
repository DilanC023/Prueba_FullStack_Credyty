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
    public class VehiculoServicio : IVehiculoServicio
    {
        private readonly IVehiculoRepositorio _vehiculoRepositorio;
        private readonly IMapper _mapper;

        public VehiculoServicio(IVehiculoRepositorio vehiculoRepositorio, IMapper mapper)
        {
            _vehiculoRepositorio = vehiculoRepositorio;
            _mapper = mapper;
        }

        public async Task<VehiculoDTO> ObtenerVehiculoPorId(int? vehiculoId = null)
        {
            var vehiculo = await _vehiculoRepositorio.ConsultarVehiculoPorID(vehiculoId);
            return _mapper.Map<VehiculoDTO>(vehiculo.FirstOrDefault());
        }
        public async Task<List<VehiculoDTO>> ObtenerVehiculos()
        {
            var vehiculo = await _vehiculoRepositorio.ConsultarVehiculoPorID();
            return _mapper.Map<List<VehiculoDTO>>(vehiculo);
        }
        public async Task<VehiculoDTO> ObtenerVehiculoPorPlaca(string Placa)
        {
            var vehiculo = await _vehiculoRepositorio.ConsultarVehiculoPorPlaca(Placa);
            return _mapper.Map<VehiculoDTO>(vehiculo.FirstOrDefault());
        }
        public async Task<VehiculoDTO> AdicionarVehiculo(VehiculoDTO vehiculoDto)
        {
            var entidad = _mapper.Map<VehiculoEntidad>(vehiculoDto);
            var nuevoVehiculo = await _vehiculoRepositorio.AdicionarVehiculo(entidad);
            return _mapper.Map<VehiculoDTO>(nuevoVehiculo);
        }
        public async Task<bool> ModificarVehiculo(VehiculoDTO vehiculoDto, int? id = null)
        {
            var consultarVehiculo = await ObtenerVehiculoPorId(id);
            if (consultarVehiculo == null)
                throw new Exception("No se encontro el Vehículo");
            var entidad = _mapper.Map<VehiculoEntidad>(vehiculoDto);
            return await _vehiculoRepositorio.ModificarVehiculo(entidad);
        }
        public async Task<bool> EliminarVehiculo(int id)
        {
            return await _vehiculoRepositorio.EliminarVehiculo(id);
        }
    }
}
