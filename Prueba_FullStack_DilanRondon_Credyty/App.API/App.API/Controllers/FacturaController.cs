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
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaServicio _facturaServicio;

        public FacturaController(IFacturaServicio facturaServicio)
        {
            _facturaServicio = facturaServicio;
        }

        /// <summary>
        /// Obtener todas las facturas
        /// </summary>
        [HttpGet("facturas")]
        public async Task<IActionResult> ObtenerFacturas()
        {
            try
            {
                var facturas = await _facturaServicio.ObtenerFacturas();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtener una factura por ID
        /// </summary>
        [HttpGet("factura/{id}")]
        public async Task<IActionResult> ObtenerFacturaPorId(int id)
        {
            try
            {
                var factura = await _facturaServicio.ObtenerFacturaPorId(id);
                if (factura == null)
                    return NotFound("Factura no encontrada");

                return Ok(factura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Registrar una nueva factura
        /// </summary>
        [HttpPost("factura")]
        public async Task<IActionResult> AdicionarFactura([FromBody] FacturaDTO facturaDto)
        {
            try
            {
                var resultado = await _facturaServicio.AdicionarFactura(facturaDto);
                if (!resultado)
                    return BadRequest(false);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, false);
            }
        }
    }
}
