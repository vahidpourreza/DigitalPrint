using Framework.Domain.Events;

namespace Framework.Domain.Entities;


public abstract class BaseAggregateRoot<TId>
{
    private readonly List<IEvent> _events;
    public TId Id { get; protected set; }
    //public long Version { get; private set; } = -1;
    protected BaseAggregateRoot() => _events = new List<IEvent>();
    protected void HandleEvent(IEvent @event)
    {
        SetStateByEvent(@event);
        ValidateInvariants();
        _events.Add(@event);
       // Version++;
    }
    protected abstract void SetStateByEvent(IEvent @event);
    public IEnumerable<IEvent> GetEvents() => _events.AsEnumerable();
    public void ClearEvents() => _events.Clear();
    protected abstract void ValidateInvariants();
}
