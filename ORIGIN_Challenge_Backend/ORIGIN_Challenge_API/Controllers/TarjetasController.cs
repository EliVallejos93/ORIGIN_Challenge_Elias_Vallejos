using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORIGIN_Challenge_API.Data;
using ORIGIN_Challenge_API.Services;
using System;

namespace ORIGIN_Challenge_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarjetasController : ControllerBase
    {
        private readonly ITarjetasService _tarjetaService;

        public TarjetasController(ITarjetasService tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }

        [HttpGet("InsertarDatosAleatorios")]
        public async Task<IActionResult> InsertarDatosAleatorios()
        {
            try
            {
                _tarjetaService.AgregarTarjetaAleatoria();
                return Ok(new { code = 200, message = "Datos aleatorios de Tarjeta insertados correctamente", data = new { } });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar datos aleatorios: {ex.Message}");
            }
        }

        [HttpGet("VerificarTarjeta")]
        public async Task<IActionResult> VerificarTarjeta(string numeroTarjeta)
        {
            try
            {
                var tarjeta = _tarjetaService.VerificarTarjeta(numeroTarjeta);
                _tarjetaService.ResetearConteoPin();
                return Ok(new { code = 200, message = "Numero de Tarjeta correcto", data = new { } });
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tarjeta no encontrada");
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(423, ex.Message);
            }
            catch (Exception ex)
            {
                return ex.HResult switch
                {
                    400 => BadRequest(ex.Message),
                    401 => Unauthorized(ex.Message),
                    404 => NotFound(ex.Message),
                    423 => StatusCode(423, ex.Message),
                    _ => StatusCode(500, "Error interno del servidor"),
                };
            }
        }

        [HttpGet("VerificarPin")]
        public async Task<IActionResult> VerificarPin(string numeroTarjeta, string numeroPin)
        {
            try
            {
                _tarjetaService.VerificarPin(numeroTarjeta, numeroPin);

                return Ok(new
                {
                    code = 200,
                    message = "PIN correcto",
                    data = new { }
                });
            }
            catch (Exception ex)
            {
                return ex.HResult switch
                {
                    400 => BadRequest(ex.Message),
                    401 => Unauthorized(ex.Message),
                    404 => NotFound(ex.Message),
                    423 => StatusCode(423, ex.Message),
                    _ => StatusCode(500, "Error interno del servidor"),
                };
            }
        }
    }
}
