using App.Servicios.DTOs;
using App.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoServicio _vehiculoServicio;

        public VehiculoController(IVehiculoServicio vehiculoServicio)
        {
            _vehiculoServicio = vehiculoServicio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiculoDTO>>> ObtenerVehiculos()
        {
            return Ok(await _vehiculoServicio.ObtenerVehiculos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoDTO>> ObtenerVehiculoPorId(int id)
        {
            var vehiculo = await _vehiculoServicio.ObtenerVehiculoPorId(id);
            return Ok(vehiculo);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarVehiculo([FromBody] VehiculoDTO vehiculo)
        {
            await _vehiculoServicio.AdicionarVehiculo(vehiculo);
            return Ok("Vehículo registrado.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarVehiculo(int? id, [FromBody] VehiculoDTO vehiculo)
        {
            await _vehiculoServicio.ModificarVehiculo(vehiculo, id);
            return Ok("Vehículo actualizado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVehiculo(int id)
        {
            await _vehiculoServicio.EliminarVehiculo(id);
            return Ok("Vehículo eliminado.");
        }
    }
}
