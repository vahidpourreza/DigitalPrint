using System.Text;
using EventStore.ClientAPI;
using Framework.Domain.Data;
using Framework.Domain.Events;
using Newtonsoft.Json;

namespace DigitalPrint.Infrastructures.Data.EventStore
{
    public class DigitalPrintEventSource : IEventSource
    {
        private readonly IEventStoreConnection _connection;

        public DigitalPrintEventSource(IEventStoreConnection connection)
        {
            _connection = connection;
          //  _connection.ConnectAsync().Wait();
        }
        public void Save<TEvent>(string aggregateName, string streamId, IEnumerable<TEvent> events) where TEvent : IEvent
        {
            if (!events.Any()) return;

            var changes = events
                .Select(@event =>
                    new EventData(
                        eventId: Guid.NewGuid(),
                        type: @event.GetType().Name,
                        isJson: true,
                        data: Serialize(@event),
                        metadata: Serialize(new EventMetadata { ClrType = @event.GetType().AssemblyQualifiedName })
                    ))
                .ToArray();

            if (!changes.Any()) return;
            var streamName = $"{aggregateName} - {streamId}";

            _connection.AppendToStreamAsync(
                streamName,
                ExpectedVersion.Any,
                changes).Wait();


        }
        private static byte[] Serialize(object data) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    }

}
