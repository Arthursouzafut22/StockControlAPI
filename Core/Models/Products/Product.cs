using ControleMercadoria.Core.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleMercadoria.Core.Models.Products
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal PriceCost { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [Required]
        public int StockQuantity { get; set; } = 0;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
