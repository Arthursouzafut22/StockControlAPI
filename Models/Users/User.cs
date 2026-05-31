using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleMercadoria.Models.Users
{
    [Index(nameof(Email), IsUnique = true)]
    [Table("Users")]
    public class User

    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string SenhaHash { get; set; } = string.Empty;

        [Required]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
