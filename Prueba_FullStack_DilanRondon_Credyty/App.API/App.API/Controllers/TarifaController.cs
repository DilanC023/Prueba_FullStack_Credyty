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
    public class TarifaController : ControllerBase
    {
        private readonly ITarifaServicio _tarifaServicio;

        public TarifaController(ITarifaServicio tarifaServicio)
        {
            _tarifaServicio = tarifaServicio;
        }

        [HttpGet("tarifas")]
        public async Task<ActionResult<IEnumerable<TarifaDTO>>> ObtenerTarifas()
        {
            return Ok(await _tarifaServicio.ObtenerTarifas());
        }
        [HttpGet("tarifa/{id}")]
        public async Task<ActionResult<TarifaDTO>> ObtenerTarifaPorID(int? id)
        {
            return Ok(await _tarifaServicio.ObtenerTarifaPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTarifa([FromBody] TarifaDTO tarifa)
        {
            await _tarifaServicio.AdicionarTarifa(tarifa);
            return Ok("Tarifa registrada.");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarTarifa(int? id, [FromBody] TarifaDTO tarifa)
        {
            await _tarifaServicio.ModificarTarifa(tarifa, id);
            return Ok("Tarifa Modificada.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTarifa(int id)
        {
            await _tarifaServicio.EliminarTarifa(id);
            return Ok("Tarifa Modificada.");
        }
    }
}
