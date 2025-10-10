using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public string Refresh_Token { get; set; } = string.Empty;
        
        [Required]
        public string JwtToken { get; set; } = string.Empty;
        
        [Required]
        public DateTime RefreshTokenExpirationTime { get; set; }
        
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
