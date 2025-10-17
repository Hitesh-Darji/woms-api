using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Enums;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class WorkOrderAssignmentRepository : Repository<WorkOrderAssignment>, IWorkOrderAssignmentRepository
    {
        public WorkOrderAssignmentRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WorkOrderAssignment>> GetByWorkOrderIdAsync(Guid workOrderId, CancellationToken cancellationToken = default)
        {
            return await _context.WorkOrderAssignments
                .AsNoTracking()
                .Where(wa => wa.WorkOrderId == workOrderId && !wa.IsDeleted)
                .OrderByDescending(wa => wa.AssignedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<WorkOrderAssignment>> GetByTechnicianIdAsync(string technicianId, CancellationToken cancellationToken = default)
        {
            return await _context.WorkOrderAssignments
                .AsNoTracking()
                .Where(wa => wa.TechnicianId == technicianId && !wa.IsDeleted)
                .Include(wa => wa.WorkOrder)
                .OrderByDescending(wa => wa.AssignedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<WorkOrderAssignment?> GetActiveAssignmentAsync(Guid workOrderId, CancellationToken cancellationToken = default)
        {
            return await _context.WorkOrderAssignments
                .AsNoTracking()
                .Where(wa => wa.WorkOrderId == workOrderId && 
                           !wa.IsDeleted && 
                           (wa.Status == AssignmentStatus.Assigned || wa.Status == AssignmentStatus.Accepted))
                .OrderByDescending(wa => wa.AssignedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> HasActiveAssignmentAsync(Guid workOrderId, CancellationToken cancellationToken = default)
        {
            return await _context.WorkOrderAssignments
                .AsNoTracking()
                .AnyAsync(wa => wa.WorkOrderId == workOrderId && 
                              !wa.IsDeleted && 
                              (wa.Status == AssignmentStatus.Assigned || wa.Status == AssignmentStatus.Accepted), cancellationToken);
        }
    }
}
