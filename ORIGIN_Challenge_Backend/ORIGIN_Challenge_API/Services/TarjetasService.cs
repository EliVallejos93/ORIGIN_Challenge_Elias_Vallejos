using ORIGIN_Challenge_Backend.Data.UnitOfWork;
using ORIGIN_Challenge_Backend.Models;
using System.Text.Json;

namespace ORIGIN_Challenge_Backend.Services
{
    public class TarjetasService : ITarjetasService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPinService _pinService;

        public TarjetasService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IPinService pinService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
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
            {
                throw new KeyNotFoundException("Tarjeta no encontrada");
            }
            if (tarjeta.Bloqueada)
            {
                throw new InvalidOperationException("La tarjeta está bloqueada");
            }

            return tarjeta;
        }

        public void ActualizarTarjeta(Tarjeta tarjeta)
        {
            _unitOfWork.Tarjetas.Update(tarjeta);
            _unitOfWork.Save();
            _httpContextAccessor.HttpContext.Items["Tarjeta"] = tarjeta;
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
                    throw Excepcion.TarjetaBloqueadaException("La Tarjeta ha sido bloqueada");
                }
                throw Excepcion.UnauthorizedException("El PIN es incorrecto");
            }
        }
    }

}
