using System.Diagnostics.CodeAnalysis;

namespace SimonGeering.Framework.Primitives;

public abstract class ValueObject : IEqualityComparer<ValueObject>, IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public bool Equals(ValueObject? other)
        => other is not null && ValuesAreEqual(other);

    public override bool Equals(object? obj)
        => obj is ValueObject other && ValuesAreEqual(other);

    public bool Equals(ValueObject? x, ValueObject? y)
        => x is not null && x.Equals(y);

    public override int GetHashCode()
        => GetAtomicValues().Aggregate(default(int), HashCode.Combine);

    public int GetHashCode([DisallowNull] ValueObject obj)
        => obj.GetHashCode();

    private bool ValuesAreEqual(ValueObject other)
        => GetAtomicValues().SequenceEqual(other.GetAtomicValues());
}
