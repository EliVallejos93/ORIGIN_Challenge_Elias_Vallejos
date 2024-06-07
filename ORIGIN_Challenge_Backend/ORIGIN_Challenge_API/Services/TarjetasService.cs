using ORIGIN_Challenge_API.Data.UnitOfWork;
using ORIGIN_Challenge_API.Models;
using System.Text.Json;

namespace ORIGIN_Challenge_API.Services
{
    public class TarjetasService : ITarjetasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPinService _pinService;

        public TarjetasService(IUnitOfWork unitOfWork, IPinService pinService)
        {
            _unitOfWork = unitOfWork;
            _pinService = pinService;
        }

        public void AgregarTarjetaAleatoria()
        {
            _unitOfWork.Tarjetas.InsertarDatosAleatorios();
        }

        public Tarjeta VerificarTarjeta(string numeroTarjeta)
        {
            var tarjeta = _unitOfWork.Tarjetas.GetByNumero(numeroTarjeta);

            if (tarjeta == null)
                throw MiExcepcion.TarjetaNoEncontradaException("Tarjeta no encontrada");
            if (tarjeta.Bloqueada)
                throw MiExcepcion.TarjetaBloqueadaException("La tarjeta está bloqueada");

            return tarjeta;
        }

        public void ActualizarTarjeta(Tarjeta tarjeta)
        {
            _unitOfWork.Tarjetas.Update(tarjeta);
            _unitOfWork.Save();
        }

        public void ResetearConteoPin()
        {
            _pinService.Reset();
        }

        public void VerificarPin(string numeroTarjeta, string numeroPin)
        {
            var tarjeta = VerificarTarjeta(numeroTarjeta);
            if (tarjeta.Pin != numeroPin)
            {
                if (_pinService.IntentoLimiteAlcanzado())
                {
                    tarjeta.Bloqueada = true;
                    _unitOfWork.Tarjetas.Update(tarjeta);
                    throw MiExcepcion.TarjetaBloqueadaException("La Tarjeta ha sido bloqueada");
                }
                throw MiExcepcion.UnauthorizedException("El PIN es incorrecto");
            }
        }
    }

}
