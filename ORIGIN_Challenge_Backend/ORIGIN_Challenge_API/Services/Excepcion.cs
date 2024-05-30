namespace ORIGIN_Challenge_Backend.Services
{
    public class Excepcion : Exception
    {
        public Excepcion(string message) : base(message) { }

        // Método estático para crear UnauthorizedPinException
        public static Excepcion UnauthorizedException(string message)
        {
            return new Excepcion(message) { HResult = 401 };  // HResult para representar Unauthorized
        }

        // Método estático para crear TarjetaNoEncontradaException
        public static Excepcion TarjetaNoEncontradaException(string message)
        {
            return new Excepcion(message) { HResult = 404 };  // HResult para representar NotFound
        }

        // Método estático para crear TarjetaBloqueadaException
        public static Excepcion TarjetaBloqueadaException(string message)
        {
            return new Excepcion(message) { HResult = 423 };  // HResult para representar Locked
        }
    }

}
