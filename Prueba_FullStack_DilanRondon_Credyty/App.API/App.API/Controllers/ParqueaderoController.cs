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
    public class ParqueaderoController : ControllerBase
    {
        private readonly IParqueaderoServicio _parqueaderoServicio;

        public ParqueaderoController(IParqueaderoServicio parqueaderoServicio)
        {
            _parqueaderoServicio = parqueaderoServicio;
        }

        /// <summary>
        /// Obtener todos los ingresos del parqueadero
        /// </summary>
        [HttpGet("ingresos")]
        public async Task<IActionResult> ObtenerIngresos()
        {
            try
            {
                var ingresos = await _parqueaderoServicio.ObtenerIngresos();
                return Ok(ingresos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtener un ingreso por ID
        /// </summary>
        [HttpGet("ingreso/{id}")]
        public async Task<IActionResult> ObtenerIngresoPorId(int id)
        {
            try
            {
                var ingreso = await _parqueaderoServicio.ObtenerIngresoPorId(id);
                if (ingreso == null)
                    return NotFound("Ingreso no encontrado");

                return Ok(ingreso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Registrar un nuevo ingreso al parqueadero
        /// </summary>
        [HttpPost("ingreso")]
        public async Task<IActionResult> RegistrarIngreso([FromBody] ParqueaderoDTO parqueaderoDto)
        {
            try
            {
                parqueaderoDto.HoraIngreso = DateTime.Now;
                var resultado = await _parqueaderoServicio.RegistrarIngreso(parqueaderoDto);
                if (!resultado)
                    return BadRequest(false); 
                return Ok(true); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, false);
            }
        }

        /// <summary>
        /// Modificar un ingreso existente
        /// </summary>
        [HttpPut("ingreso/{id}")]
        public async Task<IActionResult> ModificarIngreso(int id, [FromBody] ParqueaderoDTO parqueaderoDto)
        {
            try
            {
                parqueaderoDto.HoraSalida = DateTime.Now;
                var resultado = await _parqueaderoServicio.ModificarIngreso(parqueaderoDto, id);
                if (!resultado)
                    return NotFound(false);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, false);
            }
        }

        /// <summary>
        /// Eliminar un ingreso por ID
        /// </summary>
        [HttpDelete("ingreso/{id}")]
        public async Task<IActionResult> EliminarIngreso(int id)
        {
            try
            {
                var resultado = await _parqueaderoServicio.EliminarIngreso(id);
                if (!resultado)
                    return NotFound("Ingreso no encontrado");

                return Ok("Ingreso eliminado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
