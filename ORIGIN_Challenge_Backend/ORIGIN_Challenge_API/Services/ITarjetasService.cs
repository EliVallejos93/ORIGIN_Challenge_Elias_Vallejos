using Microsoft.AspNetCore.Mvc;
using ORIGIN_Challenge_Backend.Models;

namespace ORIGIN_Challenge_Backend.Services
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
