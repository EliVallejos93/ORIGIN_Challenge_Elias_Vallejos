using Microsoft.EntityFrameworkCore;
using ORIGIN_Challenge_API.Data.UnitOfWork;
using ORIGIN_Challenge_API.DTOs;
using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.Services
{
    public class OperacionesService : IOperacionesService
    {
        private IUnitOfWork _unitOfWork;
        public OperacionesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TarjetaOperacionDto Balance(string numeroTarjeta)
        {
            var tarjeta = _unitOfWork.Tarjetas.GetByNumeroConOperaciones(numeroTarjeta);

            if (tarjeta == null)
                throw MiExcepcion.TarjetaNoEncontradaException("Tarjeta no encontrada");

            var operacion = new Operaciones
            {
                IdTarjeta = tarjeta.IdTarjeta,
                Fecha = DateTime.Now,
                CodigoOperacion = new Random().Next(100000000, 999999999),
                CantidadRetiro = 0,
                Balance = tarjeta.DineroEnCuenta
            };

            _unitOfWork.Operaciones.Add(operacion);
            _unitOfWork.Save();

            TarjetaOperacionDto tarjetaOperacionDto = new TarjetaOperacionDto();
            tarjetaOperacionDto.T_DineroEnCuenta = tarjeta.DineroEnCuenta;
            tarjetaOperacionDto.T_FechaVencimiento = tarjeta.FechaVencimiento;
            tarjetaOperacionDto.Operaciones = tarjeta.Operaciones.Select(o => new Operaciones
            {
                Fecha = o.Fecha,
                CodigoOperacion = o.CodigoOperacion,
                CantidadRetiro = o.CantidadRetiro,
                Balance = o.Balance,
            }).ToList();

            return tarjetaOperacionDto;
        }

        public void Retiro(string numeroTarjeta, string cantidadRetiro)
        {
            if (!decimal.TryParse(cantidadRetiro, out decimal cantidadRetiroDecimal))
                throw MiExcepcion.BadRequestException("El valor de cantidad Retiro no es válido");

            var tarjeta = _unitOfWork.Tarjetas.GetByNumeroConOperaciones(numeroTarjeta);

            if (tarjeta == null)
                throw MiExcepcion.TarjetaNoEncontradaException("Tarjeta no encontrada");

            if (cantidadRetiroDecimal > tarjeta.DineroEnCuenta)
                throw MiExcepcion.BadRequestException("Fondos insuficientes para completar la transacción");


            tarjeta.DineroEnCuenta -= cantidadRetiroDecimal;

            var operacion = new Operaciones
            {
                IdTarjeta = tarjeta.IdTarjeta,
                Fecha = DateTime.Now,
                CodigoOperacion = new Random().Next(100000000, 999999999),
                CantidadRetiro = cantidadRetiroDecimal,
                Balance = tarjeta.DineroEnCuenta
            };

            _unitOfWork.Operaciones.Add(operacion);
            _unitOfWork.Save();
        }
    }
}
