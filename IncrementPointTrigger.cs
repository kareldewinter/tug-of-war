using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Linq;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Net;

namespace TugOfWar
{
    public static class IncrementPointTrigger
    {
        [FunctionName("IncrementPointTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestMessage req,
            [Queue("scorequeue", Connection = "AzureWebJobsStorage")] ICollector<string> outputQueueMessage,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            // Get the TeamId
            string teamId = data?.teamId;

            // Queue Point
            outputQueueMessage.Add(teamId);

            return req.CreateResponse(HttpStatusCode.OK);
        }

    }
}
