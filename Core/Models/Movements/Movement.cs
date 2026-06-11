using ControleMercadoria.Core.Enums;
using ControleMercadoria.Core.Models.Products;
using ControleMercadoria.Core.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleMercadoria.Core.Models.Movements
{
    [Table("Movements")]
    public class Movement
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        [Required]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        [Required]
        public MovementType Type { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public decimal UnitValue { get; set; }

        [NotMapped]
        public decimal TotalValue => Amount * UnitValue;

        [MaxLength(500)]
        public string? Observation { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

