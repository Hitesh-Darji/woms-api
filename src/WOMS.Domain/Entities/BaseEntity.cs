using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WOMS.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedOn { get; set; }
        
        public Guid? CreatedBy { get; set; }
        
        public Guid? UpdatedBy { get; set; }
        
        public bool IsDeleted { get; set; } = false;
        
        public Guid? DeletedBy { get; set; }
        
        public DateTime? DeletedOn { get; set; }
    }
}
