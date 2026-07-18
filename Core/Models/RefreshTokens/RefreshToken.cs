using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleMercadoria.Core.Models.Users;

namespace ControleMercadoria.Core.Models.RefreshToken
{
    public class RefreshToken
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public bool IsRevoked { get; set; }

        [Required]
        public DateTime ExpiresAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public long UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
    }
}