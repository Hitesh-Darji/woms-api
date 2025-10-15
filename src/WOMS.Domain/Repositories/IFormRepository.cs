using System;
using System.Threading;
using System.Threading.Tasks;
using WOMS.Domain.Entities;

namespace WOMS.Domain.Repositories
{
    public interface IFormTemplateRepository : IRepository<FormTemplate>
    {
        Task<FormTemplate?> GetByIdWithSectionsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<FormTemplate?> GetByIdWithSectionsAndFieldsAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<FormTemplate>> GetAllWithSectionsAndFieldsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<FormTemplate>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
        Task<IEnumerable<FormTemplate>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);
        Task<IEnumerable<FormTemplate>> GetActiveTemplatesAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameExcludingIdAsync(string name, Guid excludeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<FormTemplate>> GetSoftDeletedAsync(CancellationToken cancellationToken = default);
        Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default);
    }

    public interface IFormSectionRepository : IRepository<FormSection>
    {
        Task<IEnumerable<FormSection>> GetByFormTemplateIdAsync(Guid formTemplateId, CancellationToken cancellationToken = default);
        Task<IEnumerable<FormSection>> GetByFormTemplateIdOrderedAsync(Guid formTemplateId, CancellationToken cancellationToken = default);
        Task<FormSection?> GetByIdWithFieldsAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteByFormTemplateIdAsync(Guid formTemplateId, CancellationToken cancellationToken = default);
    }

    public interface IFormFieldRepository : IRepository<FormField>
    {
        Task<IEnumerable<FormField>> GetByFormSectionIdAsync(Guid formSectionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<FormField>> GetByFormSectionIdOrderedAsync(Guid formSectionId, CancellationToken cancellationToken = default);
        Task<IEnumerable<FormField>> GetByFormTemplateIdAsync(Guid formTemplateId, CancellationToken cancellationToken = default);
        Task DeleteByFormSectionIdAsync(Guid formSectionId, CancellationToken cancellationToken = default);
        Task DeleteByFormTemplateIdAsync(Guid formTemplateId, CancellationToken cancellationToken = default);
    }
}

