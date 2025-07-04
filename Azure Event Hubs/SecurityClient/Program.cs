using Azure.Messaging.EventHubs.Producer;
using EventGeneratorLibrary;
using EventGeneratorLibrary.Enums;
using System.Text.Json;
using dotenv.net;

DotEnv.Load();

await StartEventGenerating();


async Task StartEventGenerating()
{

    var connectionString = Environment.GetEnvironmentVariable("EVENT_HUB_CONNECTION_STRING");
    var eventHubName = "sensordata";

    await using (var producer = new EventHubProducerClient(connectionString, eventHubName))
    {
        while (true)
        {

            var events = EventGenerator.GetSensorEvents(Sensors.DoorSensor, 900);

            var eventBatch = await producer.CreateBatchAsync();

            foreach (var sensorEvent in events)
            {
                var eventAddedSuccessully = eventBatch.TryAdd(new Azure.Messaging.EventHubs.EventData(JsonSerializer.Serialize(sensorEvent)));
                if (!eventAddedSuccessully)
                {
                    if (eventBatch.Count > 0)
                    {
                        await producer.SendAsync(eventBatch);
                        Console.WriteLine($"Batch data sent for {eventBatch.Count} events out of {events.Count()} events because size limit reached");
                        eventBatch = await producer.CreateBatchAsync();

                    }
                    else
                    {
                        // event size is too big and cannot be added. Event needs to be skipped
                        //log the error
                    }
                }
            }
            if(eventBatch.Count > 0)
            {
                await producer.SendAsync(eventBatch);
            }
            eventBatch.Dispose();


            //await Task.Delay(5000);
        }
    }
}
