using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;

namespace WOMS.Infrastructure.Data
{
    public class WomsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public WomsDbContext(DbContextOptions<WomsDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
           

            // Configure ApplicationUser self references
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasOne(u => u.UpdatedByUser)
                      .WithMany()
                      .HasForeignKey(u => u.UpdatedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(u => u.DeletedByUser)
                      .WithMany()
                      .HasForeignKey(u => u.DeletedBy)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Address).IsRequired().HasMaxLength(500);
                entity.Property(u => u.City).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PostalCode).IsRequired().HasMaxLength(100);
            });
        }
    }
}
