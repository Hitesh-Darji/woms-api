using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        [Required]
        public bool IsClient { get; set; }
        public string? Description { get; set; }
        public Guid? CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        public virtual ApplicationUser? UpdatedByUser { get; set; }
        [ForeignKey(nameof(DeletedBy))]
        public virtual ApplicationUser? DeletedByUser { get; set; }
    }
}
