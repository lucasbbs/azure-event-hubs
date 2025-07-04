﻿using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using dotenv.net;
using Azure.Storage.Blobs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventConsumerClient
{
    public class EventProcessor
    {
        ConcurrentDictionary<string, int> partitionEventCount = new ConcurrentDictionary<string, int>();
        public async Task StartEventProcessing(CancellationToken cToken)
        {
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;
            var storageConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            var containerName = "sensorlogcheckpoint";

            var storageClient = new BlobContainerClient(storageConnectionString, containerName);

            var eventHubsConnectionString = Environment.GetEnvironmentVariable("EVENT_HUBS_CONNECTION_STRING");

            var eventHubName = "sensordata";

            var processor = new EventProcessorClient(storageClient,consumerGroup,eventHubsConnectionString,eventHubName);

            processor.ProcessEventAsync += HandleEventProcessing;
            processor.ProcessErrorAsync += HandleEventError;

            try
            {
                await processor.StartProcessingAsync(cToken);
                await Task.Delay(Timeout.Infinite, cToken);
            }
            catch
            {
                //log errors
            }
            finally
            {
                await processor.StopProcessingAsync();
                processor.ProcessEventAsync -= HandleEventProcessing;
                processor.ProcessErrorAsync -= HandleEventError;
            }

        }
        async Task HandleEventProcessing(ProcessEventArgs args)
        {
            try
            {
                if (args.CancellationToken.IsCancellationRequested)
                {
                    return;
                }

                string partition = args.Partition.PartitionId;
                byte[] eventBody = args.Data.EventBody.ToArray();

               
               
                int eventsSinceLastCheckpoint = partitionEventCount.AddOrUpdate(
                   key: partition,
                   addValue: 1,
                   updateValueFactory: (_, currentCount) => currentCount + 1);
                Console.WriteLine($"Events since last checkpoint: {eventsSinceLastCheckpoint}");
                if (eventsSinceLastCheckpoint >= 50)
                {
                    await args.UpdateCheckpointAsync();
                    partitionEventCount[partition] = 0;
                }

            }
            catch
            {

            }
        }

        Task HandleEventError(ProcessErrorEventArgs args)
        {
            Console.WriteLine("Error in the EventProcessorClient");
            Console.WriteLine($"\tOperation: { args.Operation }");
            Console.WriteLine($"\tException: { args.Exception }");
            Console.WriteLine("");
            return Task.CompletedTask;
        }
    }
}
