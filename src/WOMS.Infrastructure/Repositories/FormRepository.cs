using Microsoft.EntityFrameworkCore;
using WOMS.Domain.Entities;
using WOMS.Domain.Repositories;
using WOMS.Infrastructure.Data;

namespace WOMS.Infrastructure.Repositories
{
    public class FormTemplateRepository : Repository<FormTemplate>, IFormTemplateRepository
    {
        public FormTemplateRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<FormTemplate?> GetByIdWithSectionsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(ft => ft.FormSections)
                .FirstOrDefaultAsync(ft => ft.Id == id && !ft.IsDeleted, cancellationToken);
        }

        public async Task<FormTemplate?> GetByIdWithSectionsAndFieldsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(ft => ft.FormSections)
                    .ThenInclude(fs => fs.FormFields)
                .FirstOrDefaultAsync(ft => ft.Id == id && !ft.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<FormTemplate>> GetAllWithSectionsAndFieldsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(ft => ft.FormSections)
                    .ThenInclude(fs => fs.FormFields)
                .Where(ft => !ft.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<FormTemplate>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
        {
            return await FindAsync(ft => ft.Category == category && !ft.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<FormTemplate>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
        {
            return await FindAsync(ft => ft.Status == status && !ft.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<FormTemplate>> GetActiveTemplatesAsync(CancellationToken cancellationToken = default)
        {
            return await FindAsync(ft => ft.IsActive && !ft.IsDeleted, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(ft => ft.Name == name && !ft.IsDeleted, cancellationToken);
        }

        public async Task<bool> ExistsByNameExcludingIdAsync(string name, Guid excludeId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(ft => ft.Name == name && ft.Id != excludeId && !ft.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<FormTemplate>> GetSoftDeletedAsync(CancellationToken cancellationToken = default)
        {
            return await FindAsync(ft => ft.IsDeleted, cancellationToken);
        }

        public async Task<bool> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var formTemplate = await _dbSet.FirstOrDefaultAsync(ft => ft.Id == id && ft.IsDeleted, cancellationToken);
            if (formTemplate == null)
            {
                return false;
            }

            formTemplate.IsDeleted = false;
            formTemplate.DeletedBy = null;
            formTemplate.DeletedOn = null;
            formTemplate.UpdatedOn = DateTime.UtcNow;

            await UpdateAsync(formTemplate, cancellationToken);
            return true;
        }
    }

    public class FormSectionRepository : Repository<FormSection>, IFormSectionRepository
    {
        public FormSectionRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FormSection>> GetByFormTemplateIdAsync(Guid formTemplateId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(fs => fs.FormTemplateId == formTemplateId && !fs.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<FormSection>> GetByFormTemplateIdOrderedAsync(Guid formTemplateId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(fs => fs.FormTemplateId == formTemplateId && !fs.IsDeleted)
                .OrderBy(fs => fs.OrderIndex)
                .ToListAsync(cancellationToken);
        }

        public async Task<FormSection?> GetByIdWithFieldsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(fs => fs.FormFields)
                .FirstOrDefaultAsync(fs => fs.Id == id && !fs.IsDeleted, cancellationToken);
        }

        public async Task DeleteByFormTemplateIdAsync(Guid formTemplateId, CancellationToken cancellationToken = default)
        {
            var sections = await GetByFormTemplateIdAsync(formTemplateId, cancellationToken);
            foreach (var section in sections)
            {
                await DeleteAsync(section, cancellationToken);
            }
        }
    }

    public class FormFieldRepository : Repository<FormField>, IFormFieldRepository
    {
        public FormFieldRepository(WomsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FormField>> GetByFormSectionIdAsync(Guid formSectionId, CancellationToken cancellationToken = default)
        {
            return await FindAsync(ff => ff.FormSectionId == formSectionId && !ff.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<FormField>> GetByFormSectionIdOrderedAsync(Guid formSectionId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(ff => ff.FormSectionId == formSectionId && !ff.IsDeleted)
                .OrderBy(ff => ff.OrderIndex)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<FormField>> GetByFormTemplateIdAsync(Guid formTemplateId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(ff => ff.FormSection)
                .Where(ff => ff.FormSection.FormTemplateId == formTemplateId && !ff.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteByFormSectionIdAsync(Guid formSectionId, CancellationToken cancellationToken = default)
        {
            var fields = await GetByFormSectionIdAsync(formSectionId, cancellationToken);
            foreach (var field in fields)
            {
                await DeleteAsync(field, cancellationToken);
            }
        }

        public async Task DeleteByFormTemplateIdAsync(Guid formTemplateId, CancellationToken cancellationToken = default)
        {
            var fields = await GetByFormTemplateIdAsync(formTemplateId, cancellationToken);
            foreach (var field in fields)
            {
                await DeleteAsync(field, cancellationToken);
            }
        }
    }
}

