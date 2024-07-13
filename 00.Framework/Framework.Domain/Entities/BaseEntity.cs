using Framework.Domain.Events;

namespace Framework.Domain.Entities;

public abstract class BaseEntity<TId>
{
    private readonly List<IEvent> _events = new List<IEvent>();
    private void Raise(IEvent @event) => _events.Add(@event);
    protected void HandleEvent(IEvent @event)
    {
        SetStateByEvent(@event);
        ValidateInvariants();
        Raise(@event);
    }
    protected abstract void SetStateByEvent(IEvent @event);

    public IEnumerable<IEvent> GetChanges() => _events.AsEnumerable();
    public void ClearChanges() => _events.Clear();
    public TId Id { get; set; }

    protected abstract void ValidateInvariants();

}