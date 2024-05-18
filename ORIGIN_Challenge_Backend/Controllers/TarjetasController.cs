using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORIGIN_Challenge_Backend.Models;
using ORIGIN_Challenge_Backend.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ORIGIN_Challenge_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarjetasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IntentosPinService _intentosPinService;

        public TarjetasController(ApplicationDbContext context, IntentosPinService intentosPinService)
        {
            _context = context;
            _intentosPinService = intentosPinService;
        }

        [HttpGet("InsertarDatosAleatorios")]
        public async Task<IActionResult> InsertarDatosAleatorios()
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC sp_InsertarTarjetasAleatorias");
                return Ok(new { code = 200, message = "Datos aleatorios de Tarjeta insertados correctamente", data = new { } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar datos aleatorios: {ex.Message}");
            }
        }

        [HttpGet("VerificarTarjeta")]
        public IActionResult VerificarTarjeta(string numeroTarjeta)
        {
            var tarjeta = _context.Tarjetas.SingleOrDefault(t => t.NumeroTarjeta == numeroTarjeta);
            if (tarjeta == null)
            {
                return NotFound("Tarjeta no encontrada.");
            }
            if (tarjeta.Bloqueada)
            {
                return StatusCode(423, "La tarjeta está bloqueada.");
            }
            _intentosPinService.Reset();
            return Ok(new { code = 200, message = "Numero de Tarjeta correcto", data = new { } });
        }

        [HttpGet("VerificarPin")]
        public IActionResult VerificarPin(string numeroTarjeta, string numeroPin)
        {
            var tarjeta = _context.Tarjetas.SingleOrDefault(t => t.NumeroTarjeta == numeroTarjeta);
            if (tarjeta == null)
            {
                return NotFound("Tarjeta no encontrada.");
            }
            if (tarjeta.Pin != numeroPin)
            {
                if (_intentosPinService.IntentoLimiteAlcanzado())
                {
                    tarjeta.Bloqueada = true;
                    _context.SaveChanges();
                    return StatusCode(423, "La Tarjeta ha sido bloqueada");
                }
                return Unauthorized("El PIN es incorrecto.");
            }

            return Ok(new
            {
                code = 200,
                message = "PIN correcto",
                data = new { }
            });
        }
    }
}
