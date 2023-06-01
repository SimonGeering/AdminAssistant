using AdminAssistant.DomainModel.Modules.ContactsModule;
using AdminAssistant.DomainModel.Shared;
using AdminAssistant.Infra.DAL.EntityFramework;
using AdminAssistant.Infra.DAL.EntityFramework.Model.Contacts;
using AdminAssistant.Infra.Providers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Infra.DAL.Modules.ContactsModule;

internal sealed class ContactRepository : RepositoryBase, IContactRepository
{
    public ContactRepository(IApplicationDbContext dbContext, IMapper mapper, IDateTimeProvider dateTimeProvider, IUserContextProvider userContextProvider)
        : base(dbContext, mapper, dateTimeProvider, userContextProvider)
    {
    }

    public async Task<Contact?> GetAsync(int id)
    {
        var data = await DbContext.Contacts.FirstOrDefaultAsync(x => x.ContactID == id).ConfigureAwait(false);
        return Mapper.Map<Contact>(data);
    }

    public async Task<List<Contact>> GetListAsync()
    {
        var data = await DbContext.Contacts.ToListAsync().ConfigureAwait(false);
        return Mapper.Map<List<Contact>>(data);
    }

    public async Task<Contact> SaveAsync(Contact domainObjectToSave)
    {
        var entity = Mapper.Map<ContactEntity>(domainObjectToSave);

        if (base.IsNew(domainObjectToSave))
        {
            entity.Audit = new EntityFramework.Model.Core.AuditEntity()
            {
                CreatedBy = UserContextProvider.GetCurrentUser().SignOn,
                CreatedOn = DateTimeProvider.UtcNow
            };
            DbContext.Contacts.Add(entity);
        }
        else
        {
            entity.Audit = DbContext.AuditTrail.Single(x => x.AuditID == entity.AuditID);
            entity.Audit.UpdatedBy = UserContextProvider.GetCurrentUser().SignOn;
            entity.Audit.UpdatedOn = DateTimeProvider.UtcNow;

            DbContext.Contacts.Update(entity);
        }

        await DbContext.SaveChangesAsync().ConfigureAwait(false);

        return Mapper.Map<Contact>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await DbContext.Contacts.FirstOrDefaultAsync(x => x.ContactID == id).ConfigureAwait(false);

        // TODO: make this a custom domain exception and handle in controller.
        if (entity == null || entity.ContactID != id)
            throw new ArgumentException($"Record with ID {id} not found", nameof(id));

        DbContext.Contacts.Remove(entity);
        await DbContext.SaveChangesAsync().ConfigureAwait(false);
    }
}
