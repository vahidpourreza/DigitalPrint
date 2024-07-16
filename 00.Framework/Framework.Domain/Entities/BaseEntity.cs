using Framework.Domain.Events;

namespace Framework.Domain.Entities;

public abstract class BaseEntity<TId> where TId : IEquatable<TId>
{
    public TId Id { get; protected set; }
    private Action<IEvent> _applier;
    public BaseEntity(Action<IEvent> applier)
    {
        _applier = applier;

    }
    protected BaseEntity() { }
    public void HandleEvent(IEvent @event)
    {
        SetStateByEvent(@event);
        _applier(@event);
    }

    protected abstract void SetStateByEvent(IEvent @event);


}
