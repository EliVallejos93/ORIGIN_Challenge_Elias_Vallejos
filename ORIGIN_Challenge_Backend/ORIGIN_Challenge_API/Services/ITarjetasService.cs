using Microsoft.AspNetCore.Mvc;
using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.Services
{
    public interface ITarjetasService
    {
        void AgregarTarjetaAleatoria();
        Tarjeta VerificarTarjeta(string numeroTarjeta);
        void ActualizarTarjeta(Tarjeta tarjeta);
        public void ResetearConteoPin();
        public void VerificarPin(string numeroTarjeta, string numeroPin);
    }

}
