using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class TechnicianEquipmentRepository : Repository<TechnicianEquipment>, ITechnicianEquipmentRepository
    {
        public TechnicianEquipmentRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TechnicianEquipment>> GetTechnicianEquipmentAsync(string technicianId, CancellationToken cancellationToken = default)
        {
            return await _context.TechnicianEquipment
                .AsNoTracking()
                .Where(te => te.TechnicianId == technicianId && 
                           te.Status != "Returned" && 
                           te.Status != "Lost" && 
                           !te.IsDeleted)
                .Include(te => te.Equipment)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<string>> GetTechnicianEquipmentNamesAsync(string technicianId, CancellationToken cancellationToken = default)
        {
            return await _context.TechnicianEquipment
                .AsNoTracking()
                .Where(te => te.TechnicianId == technicianId && 
                           te.Status != "Returned" && 
                           te.Status != "Lost" && 
                           !te.IsDeleted)
                .Include(te => te.Equipment)
                .Select(te => te.Equipment.Name)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasEquipmentAsync(string technicianId, string equipmentName, CancellationToken cancellationToken = default)
        {
            return await _context.TechnicianEquipment
                .AsNoTracking()
                .AnyAsync(te => te.TechnicianId == technicianId && 
                              te.Equipment.Name == equipmentName &&
                              te.Status != "Returned" && 
                              te.Status != "Lost" && 
                              !te.IsDeleted, cancellationToken);
        }
    }
}
