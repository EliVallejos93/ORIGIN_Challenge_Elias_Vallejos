using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORIGIN_Challenge_Backend.Data;
using ORIGIN_Challenge_Backend.Models;

namespace ORIGIN_Challenge_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OperacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Balance")]
        public IActionResult Balance(string numeroTarjeta)
        {
            var tarjeta = _context.Tarjetas
                .Include(t => t.Operaciones)
                .SingleOrDefault(t => t.NumeroTarjeta == numeroTarjeta);

            if (tarjeta == null)
            {
                return NotFound("Tarjeta no encontrada.");
            }

            var operacion = new Operacion
            {
                IdTarjeta = tarjeta.IdTarjeta,
                Fecha = DateTime.Now,
                CodigoOperacion = new Random().Next(100000000, 999999999),
                CantidadRetiro = 0,
                Balance = tarjeta.DineroEnCuenta
            };

            _context.Operaciones.Add(operacion);
            _context.SaveChanges();

            var operaciones = tarjeta.Operaciones.Select(o => new
            {
                o.Fecha,
                o.CodigoOperacion,
                o.CantidadRetiro,
                o.Balance
            });

            return Ok(new
            {
                code = 200,
                message = "Operacion exitosa",
                data = new
                {
                    dineroEnCuenta = tarjeta.DineroEnCuenta,
                    fechaVencimiento = tarjeta.FechaVencimiento,
                    operaciones = operaciones
                }
            });
        }

        [HttpGet("Retiro")]
        public IActionResult Retiro(string numeroTarjeta, string cantidadRetiro)
        {
            if (!decimal.TryParse(cantidadRetiro, out decimal cantidadRetiroDecimal))
            {
                return BadRequest("El valor de cantidadRetiro no es válido.");
            }

            var tarjeta = _context.Tarjetas
                .Include(t => t.Operaciones)
                .SingleOrDefault(t => t.NumeroTarjeta == numeroTarjeta);

            if (tarjeta == null)
            {
                return NotFound("Tarjeta no encontrada.");
            }

            if (cantidadRetiroDecimal > tarjeta.DineroEnCuenta)
            {
                return BadRequest("Fondos insuficientes para completar la transacción.");
            }

            tarjeta.DineroEnCuenta -= cantidadRetiroDecimal;

            var operacion = new Operacion
            {
                IdTarjeta = tarjeta.IdTarjeta,
                Fecha = DateTime.Now,
                CodigoOperacion = new Random().Next(100000000, 999999999),
                CantidadRetiro = cantidadRetiroDecimal,
                Balance = tarjeta.DineroEnCuenta
            };

            _context.Operaciones.Add(operacion);
            _context.SaveChanges();

            return Ok(new
            {
                code = 200,
                message = "Retiro exitoso",
                data = new { }
            });
        }
    }
}
