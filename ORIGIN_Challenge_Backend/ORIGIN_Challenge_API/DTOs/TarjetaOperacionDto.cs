using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.DTOs
{
    public class TarjetaOperacionDto
    {
        public decimal T_DineroEnCuenta { get; set; }
        public DateTime T_FechaVencimiento { get; set; }
        public ICollection<Operaciones> Operaciones { get; set; }

    }
}
