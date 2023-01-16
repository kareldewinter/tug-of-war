using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
using TugOfWar.Entities;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Net;
using System.Linq;

namespace TugOfWar
{
    public static class GetTeamSettings
    {
        
        // Return Team Settings
        [FunctionName("GetTeamSettings")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestMessage req,
            [Table("gamesettings", Connection = "AzureWebJobsStorage")] CloudTable inTable,
            TraceWriter log)
        {
            log.Info("### Get Team Settings Triggered ###");

            // Get Team Settings
            TableQuery<TeamSetting> query = new TableQuery<TeamSetting>().Take(1);
            TeamSetting ts = inTable.ExecuteQuery(query).FirstOrDefault();

            // Return team settings
            return req.CreateResponse(HttpStatusCode.OK, ts);

        }
    }
}
