namespace SimonGeering.Framework.Primitives;

public interface IAggregateRoot<out TId> : IDomainEntity<TId>
    where TId : Id;

public abstract class AggregateRoot<TId>(TId id) : DomainEntity<TId>(id), IAggregateRoot<TId>
    where TId : Id;
