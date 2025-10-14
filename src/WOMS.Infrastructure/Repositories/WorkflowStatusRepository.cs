using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class WorkflowStatusRepository : Repository<WorkflowStatus>, IWorkflowStatusRepository
    {
        public WorkflowStatusRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<WorkflowStatus>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .AsNoTracking()
                .Where(s => !s.IsDeleted && s.IsActive)
                .OrderBy(s => s.Order)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<WorkflowStatus>> GetByOrderAsync(CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .AsNoTracking()
                .Where(s => !s.IsDeleted)
                .OrderBy(s => s.Order)
                .ToListAsync(cancellationToken);
        }

        public async Task<WorkflowStatus?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await GetFirstOrDefaultAsync(s => s.Name == name && !s.IsDeleted, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var status = await GetFirstOrDefaultAsync(s => s.Name == name && !s.IsDeleted, cancellationToken);
            return status != null;
        }

        public async Task<IEnumerable<WorkflowStatusTransition>> GetTransitionsAsync(Guid workflowId, Guid fromStatusId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<WorkflowStatusTransition>()
                .AsNoTracking()
                .Where(t => t.WorkflowId == workflowId && 
                           t.FromStatusId == fromStatusId && 
                           !t.IsDeleted && 
                           t.IsActive)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> CanTransitionAsync(Guid workflowId, Guid fromStatusId, Guid toStatusId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<WorkflowStatusTransition>()
                .AsNoTracking()
                .AnyAsync(t => t.WorkflowId == workflowId &&
                              t.FromStatusId == fromStatusId && 
                              t.ToStatusId == toStatusId && 
                              !t.IsDeleted && 
                              t.IsActive, cancellationToken);
        }

        public async Task<WorkflowStatus?> GetByIdWithTransitionsAsync(Guid workflowId, Guid statusId, CancellationToken cancellationToken = default)
        {
            var status = await GetByIdAsync(statusId, cancellationToken);
            if (status == null)
                return null;

            // Note: Transitions are now workflow-specific, so we can't assign them to the status entity
            // The transitions should be returned separately or handled in a DTO
            return status;
        }
    }
}
