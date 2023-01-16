using System.Linq;
using System.Net;
using System.Net.Http;
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
using Microsoft.WindowsAzure.Storage.Table;

namespace TugOfWar
{
    public static class CreateTeamSettings
    {
        // Setup Teams
        [FunctionName("CreateTeamSettings")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "options", "post")] HttpRequestMessage req,
            [Table("gamesettings", Connection = "AzureWebJobsStorage")] ICollector<TeamSetting> outTable,
            TraceWriter log)
        {

            log.Info("### Create Team Settings Triggered ###");

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();
            string team1 = data?.team1;
            string team2 = data?.team2;

            // Create team settings
            TeamSetting ts = new TeamSetting(team1, team2);

            // Store to table
            outTable.Add(ts);

            // Return team settings
            return req.CreateResponse(HttpStatusCode.Created, ts);

        }

        public class Teams
        {
            public string team1 { get; set; }
            public string team2 { get; set; }
        }

    }
}
