using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface ITechnicianEquipmentRepository : IRepository<TechnicianEquipment>
    {
        Task<IEnumerable<TechnicianEquipment>> GetTechnicianEquipmentAsync(string technicianId, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> GetTechnicianEquipmentNamesAsync(string technicianId, CancellationToken cancellationToken = default);
        Task<bool> HasEquipmentAsync(string technicianId, string equipmentName, CancellationToken cancellationToken = default);
    }
}
