using Framework.Domain.Events;

namespace Framework.Domain.Entities;

public abstract class BaseAggregateRoot<TId>
{

    private readonly List<IEvent> _events;
    public TId Id { get; protected set; }
    public long Version { get; private set; } = -1;
    protected BaseAggregateRoot() => _events = new List<IEvent>();
    protected void HandleEvent(IEvent @event)
    {
        SetStateByEvent(@event);
        ValidateInvariants();
        _events.Add(@event);
        Version++;
    }
    protected abstract void SetStateByEvent(IEvent @event);
    public IEnumerable<IEvent> GetEvents() => _events.AsEnumerable();
    public void ClearEvents() => _events.Clear();
    protected abstract void ValidateInvariants();


    public override bool Equals(object obj)
    {
        var other = obj as BaseAggregateRoot<TId>;

        if (ReferenceEquals(other, null))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id.Equals(other.Id);
    }

    public static bool operator ==(BaseAggregateRoot<TId> a, BaseAggregateRoot<TId> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(BaseAggregateRoot<TId> a, BaseAggregateRoot<TId> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}