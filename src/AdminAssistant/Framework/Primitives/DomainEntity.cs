namespace AdminAssistant.Framework.Primitives;
public abstract class DomainEntity<TId>
    : IEquatable<DomainEntity<TId>>, IDatabasePersistable
    where TId : Id
{
    public int PrimaryKey => Id.Value;

    public TId Id { get; private init; }

    public DomainEntity(TId id) => Id = id;

    public static bool operator == (DomainEntity<TId> left, DomainEntity<TId> right)
        => left is not null && right is not null && left.Equals(right);

    public static bool operator != (DomainEntity<TId> left, DomainEntity<TId> right)
        => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (obj.GetType() != GetType())
            return false;

        if (obj is not DomainEntity<TId> domainEntity)
            return false;

        return domainEntity.Id == Id;
    }

    public bool Equals(DomainEntity<TId>? other)
    {
        if (other is null)
            return false;

        if (other.GetType() != GetType())
            return false;

        return other.Id == Id;
    }

    public override int GetHashCode()
        => Id.GetHashCode();
}
