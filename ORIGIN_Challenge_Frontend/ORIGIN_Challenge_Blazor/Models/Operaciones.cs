using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ORIGIN_Challenge_Blazor.Models
{
    public class Operaciones
    {
        public int IdOperacion { get; set; }
        public int IdTarjeta { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int CodigoOperacion { get; set; }
        public decimal? CantidadRetiro { get; set; }
        public decimal Balance { get; set; }
    }
}
