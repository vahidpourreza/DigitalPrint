using Framework.Domain.Events;

namespace DigitalPrint.Core.Domain.UserProfiles.Events;

public class UserEmailUpdated : IEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
}