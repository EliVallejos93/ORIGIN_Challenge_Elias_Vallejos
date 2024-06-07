using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORIGIN_Challenge_API.DTOs;
using ORIGIN_Challenge_API.Models;
using ORIGIN_Challenge_API.Services;

namespace ORIGIN_Challenge_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperacionesController : ControllerBase
    {
        private readonly IOperacionesService _operacionesService;

        public OperacionesController(IOperacionesService operacionesService)
        {
            _operacionesService = operacionesService;
        }

        [HttpGet("Balance")]
        public async Task<IActionResult> Balance(string numeroTarjeta)
        {
            try
            {
                TarjetaOperacionDto tarjetaOperacionDto = _operacionesService.Balance(numeroTarjeta);

                return Ok(new
                {
                    code = 200,
                    message = "Operacion exitosa",
                    data = new
                    {
                        dineroEnCuenta = tarjetaOperacionDto.T_DineroEnCuenta,
                        fechaVencimiento = tarjetaOperacionDto.T_FechaVencimiento,
                        operaciones = tarjetaOperacionDto.Operaciones
                    }
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

        [HttpGet("Retiro")]
        public async Task<IActionResult> Retiro(string numeroTarjeta, string cantidadRetiro)
        {
            try
            {
                _operacionesService.Retiro(numeroTarjeta, cantidadRetiro);

                return Ok(new
                {
                    code = 200,
                    message = "Retiro exitoso",
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
