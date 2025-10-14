using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;
using System.Text.Json;

namespace WOMS.Infrastructure.Repositories
{
    public class WorkflowRepository : Repository<Workflow>, IWorkflowRepository
    {
        public WorkflowRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Workflow>> GetAllWithNodesAsync(CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .AsNoTracking()
                .Include(w => w.Nodes)
                .Where(w => !w.IsDeleted)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Workflow?> GetByIdWithNodesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .Include(w => w.Nodes)
                .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<Workflow>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .AsNoTracking()
                .Include(w => w.Nodes)
                .Where(w => !w.IsDeleted && w.Category == category)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Workflow>> GetActiveWorkflowsAsync(CancellationToken cancellationToken = default)
        {
            return await GetQueryable()
                .AsNoTracking()
                .Include(w => w.Nodes)
                .Where(w => !w.IsDeleted && w.IsActive)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<(IEnumerable<Workflow> Workflows, int TotalCount)> GetPaginatedAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null,
            string? category = null,
            bool? isActive = null,
            string? sortBy = "CreatedAt",
            bool sortDescending = true,
            CancellationToken cancellationToken = default)
        {
            var query = GetQueryable()
                .AsNoTracking()
                .Include(w => w.Nodes)
                .Where(w => !w.IsDeleted);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Name, $"%{searchTerm}%") ||
                    EF.Functions.Like(w.Description, $"%{searchTerm}%"));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(w => w.Category == category);
            }

            if (isActive.HasValue)
            {
                query = query.Where(w => w.IsActive == isActive.Value);
            }

            // Apply sorting
            query = (sortBy?.ToLower() ?? "createdat") switch
            {
                "name" => sortDescending
                    ? query.OrderByDescending(w => w.Name)
                    : query.OrderBy(w => w.Name),
                "category" => sortDescending
                    ? query.OrderByDescending(w => w.Category)
                    : query.OrderBy(w => w.Category),
                "updatedat" => sortDescending
                    ? query.OrderByDescending(w => w.UpdatedAt)
                    : query.OrderBy(w => w.UpdatedAt),
                _ => sortDescending
                    ? query.OrderByDescending(w => w.CreatedAt)
                    : query.OrderBy(w => w.CreatedAt)
            };

            // Get total count
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination
            var workflows = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (workflows, totalCount);
        }

        public async Task<WorkflowNode?> GetNodeByIdAsync(Guid nodeId, CancellationToken cancellationToken = default)
        {
            return await _context.WorkflowNodes
                .FirstOrDefaultAsync(n => n.Id == nodeId && !n.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<WorkflowNode>> GetNodesByWorkflowIdAsync(Guid workflowId, CancellationToken cancellationToken = default)
        {
            return await _context.WorkflowNodes
                .AsNoTracking()
                .Where(n => n.WorkflowId == workflowId && !n.IsDeleted)
                .OrderBy(n => n.OrderIndex)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateNodeConnectionsAsync(Guid nodeId, List<string> connections, CancellationToken cancellationToken = default)
        {
            var node = await _context.WorkflowNodes
                .FirstOrDefaultAsync(n => n.Id == nodeId && !n.IsDeleted, cancellationToken);

            if (node == null)
                return false;

            node.Connections = JsonSerializer.Serialize(connections);
            node.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task AddNodeAsync(WorkflowNode node, CancellationToken cancellationToken = default)
        {
            await _context.WorkflowNodes.AddAsync(node, cancellationToken);
        }

        public void UpdateNode(WorkflowNode node, CancellationToken cancellationToken = default)
        {
            _context.WorkflowNodes.Update(node);

        }

        public async Task DeleteNodeAsync(WorkflowNode node, CancellationToken cancellationToken = default)
        {
            node.IsDeleted = true;
            node.DeletedOn = DateTime.UtcNow;
            _context.Set<WorkflowNode>().Update(node);
            await Task.CompletedTask;
        }
    }
}
