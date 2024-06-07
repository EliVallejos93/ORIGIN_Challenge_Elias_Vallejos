using ORIGIN_Challenge_API.DTOs;
using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.Services
{
    public interface IOperacionesService
    {
        public TarjetaOperacionDto Balance(string numeroTarjeta);
        public void Retiro(string numeroTarjeta, string cantidadRetiro);
    }
}
