using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;
using TugOfWar.Entities;

namespace TugOfWar
{
    public static class IncrementPointUpdate
    {
        [FunctionName("IncrementPointUpdate")]
        public static void Run(
            [QueueTrigger("scorequeue", Connection = "AzureWebJobsStorage")] string TeamId,
            [Table("teampoints", Connection = "AzureWebJobsStorage")] ICollector<TeamPoint> outTable,
            TraceWriter log)
        {
            log.Info("### Increment Point Update ###");

            // Create team settings
            TeamPoint tp = new TeamPoint(TeamId);

            // Store to table
            outTable.Add(tp);
        }

    }
}
