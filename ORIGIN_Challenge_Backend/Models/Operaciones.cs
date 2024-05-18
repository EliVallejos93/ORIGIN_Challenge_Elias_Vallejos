using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ORIGIN_Challenge_Backend.Models
{
    public class Operacion
    {
        [Key]
        public int IdOperacion { get; set; }

        [Required]
        public int IdTarjeta { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        public int CodigoOperacion { get; set; }

        public decimal? CantidadRetiro { get; set; }

        public decimal Balance { get; set; }


        [ForeignKey("IdTarjeta")]
        public Tarjeta Tarjeta { get; set; }
    }
}

