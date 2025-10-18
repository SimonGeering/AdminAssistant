using AdminAssistant.Infrastructure.DAL;
using AdminAssistant.Infrastructure.EntityFramework;
using AdminAssistant.Infrastructure.EntityFramework.Model.Core;
using AdminAssistant.Infrastructure.Providers;
using AdminAssistant.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdminAssistant.Modules.ContactsModule.Infrastructure.DAL;

public interface IContactRepository : IRepository<Contact, ContactId>;

internal sealed class ContactRepository(
    IApplicationDbContext dbContext,
    IDateTimeProvider dateTimeProvider,
    IUserContextProvider userContextProvider)
    : RepositoryBase(dbContext, dateTimeProvider, userContextProvider), IContactRepository
{
    public async Task<Contact?> GetAsync(ContactId id, CancellationToken cancellationToken)
    {
        var data = await DbContext.Contacts.FirstOrDefaultAsync(x => x.ContactID == id.Value, cancellationToken).ConfigureAwait(false);
        return data?.ToContact();
    }

    public async Task<List<Contact>> GetListAsync(CancellationToken cancellationToken)
    {
        var data = await DbContext.Contacts.ToListAsync(cancellationToken).ConfigureAwait(false);
        return data.ToContactList();
    }

    public async Task<Contact> SaveAsync(Contact domainObjectToSave, CancellationToken cancellationToken)
    {
        var entity = domainObjectToSave.ToContactEntity();

        if (IsNew(domainObjectToSave))
        {
            entity.Audit = new AuditEntity()
            {
                CreatedBy = UserContextProvider.GetCurrentUser().SignOn,
                CreatedOn = DateTimeProvider.UtcNow
            };
            DbContext.Contacts.Add(entity);
        }
        else
        {
            entity.Audit = await DbContext.AuditTrail.SingleAsync(x => x.AuditID == entity.AuditID, cancellationToken);
            entity.Audit.UpdatedBy = UserContextProvider.GetCurrentUser().SignOn;
            entity.Audit.UpdatedOn = DateTimeProvider.UtcNow;

            DbContext.Contacts.Update(entity);
        }

        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return entity.ToContact();
    }

    public async Task DeleteAsync(ContactId id, CancellationToken cancellationToken)
    {
        var entity = await DbContext.Contacts.FirstOrDefaultAsync(x => x.ContactID == id.Value, cancellationToken).ConfigureAwait(false);

        // TODO: make this a custom domain exception and handle in controller.
        if (entity == null || entity.ContactID != id.Value)
            throw new ArgumentException($"Record with ID {id.Value} not found", nameof(id));

        DbContext.Contacts.Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
