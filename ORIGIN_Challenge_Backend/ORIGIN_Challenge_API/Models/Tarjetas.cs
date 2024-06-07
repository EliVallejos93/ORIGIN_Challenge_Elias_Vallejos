using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ORIGIN_Challenge_API.Models
{
    public class Tarjeta
    {
        [Key]
        public int IdTarjeta { get; set; }

        [Required]
        [StringLength(16)]
        public required string NumeroTarjeta { get; set; }

        [Required]
        [StringLength(4)]
        public required string Pin { get; set; }

        public bool Bloqueada { get; set; } = false;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal DineroEnCuenta { get; set; }

        [Required]
        public DateTime FechaVencimiento { get; set; }

        public ICollection<Operaciones> Operaciones { get; set; }

    }
}

