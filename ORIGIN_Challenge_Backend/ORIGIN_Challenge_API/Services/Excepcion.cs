using System.Runtime.Serialization;

namespace ORIGIN_Challenge_API.Services
{
    public class MiExcepcion : Exception
    {
        public MiExcepcion(string message) : base(message) { }

        // Método estático para crear UnauthorizedPinException
        public static MiExcepcion UnauthorizedException(string message)
        {
            return new MiExcepcion(message) { HResult = 401 };  // HResult para representar Unauthorized
        }

        // Método estático para crear TarjetaNoEncontradaException
        public static MiExcepcion TarjetaNoEncontradaException(string message)
        {
            return new MiExcepcion(message) { HResult = 404 };  // HResult para representar NotFound
        }

        // Método estático para crear TarjetaBloqueadaException
        public static MiExcepcion TarjetaBloqueadaException(string message)
        {
            return new MiExcepcion(message) { HResult = 423 };  // HResult para representar Locked
        }

        // Método estático para crear BadRequestException
        public static MiExcepcion BadRequestException(string message)
        {
            return new MiExcepcion(message) { HResult = 400 };  // HResult para representar BadRequest
        }
    }

}
