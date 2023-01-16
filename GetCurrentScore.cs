using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TugOfWar.Entities;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Net;
using Microsoft.Azure.Cosmos.Table;
using System.Linq;

namespace TugOfWar
{
    public static class GetCurrentScore
    {
        [FunctionName("GetCurrentScore")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "options", "post", Route = null)] HttpRequestMessage req,
            [Table("teampoints", Connection = "AzureWebJobsStorage")] CloudTable inTable,
            TraceWriter log)
        {
            log.Info("### Get Current Score Triggered ###");

            dynamic data = await req.Content.ReadAsAsync<object>();
            string teamId = data?.teamId;

            // Get only the records with the corresponding teamId
            TableQuery<TeamPoint> query = new TableQuery<TeamPoint>()
                .Where(TableQuery.GenerateFilterCondition("TeamId", QueryComparisons.Equal, teamId));

            // Get the Score
            int scoreResult = inTable.ExecuteQuery(query).Count();

            TeamScore ts = new TeamScore(teamId, scoreResult);

            // Return the Score
            return req.CreateResponse(HttpStatusCode.OK, ts);
        }

    }
}
