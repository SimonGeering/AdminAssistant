using System.Diagnostics.CodeAnalysis;

namespace SimonGeering.Framework.Primitives;

public abstract class DomainEntity<TId>
    : IEqualityComparer<TId>, IEquatable<DomainEntity<TId>>, IPersistable
    where TId : Id
{
    public Id PrimaryKey => Id;

    public TId Id { get; private init; }

    protected DomainEntity(TId id) => Id = id;

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

    public bool Equals(TId? x, TId? y)
        => x is not null && x.Equals(y);

    public override int GetHashCode()
        => Id.GetHashCode();

    public int GetHashCode([DisallowNull] TId obj)
        => obj.GetHashCode();
}
