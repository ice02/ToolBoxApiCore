using Microsoft.AspNetCore.Mvc;
using Simple.OData.Client;
using System.Collections.Generic;
using ToolboxApi.Apps.V1.Model;

namespace ToolboxApi.Apps.V1
{

    [Route("api/apps/v1/items")]
    public class AppsV1Controller : Controller
    {
        // https://github.com/simple-odata-client/Simple.OData.Client/wiki/Retrieving-data

        readonly ODataClient client = new ODataClient("http://packages.nuget.org/v1/FeedService.svc/");

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            //var packages = await client
            //    .For<Package>()
            //    .Filter(x => x.Title == "Simple.OData.Client")
            //    .FindEntriesAsync();
            //foreach (var package in packages)
            //{
            //    Console.WriteLine(package.Title);
            //}

            return new[] { new Item("Item 1", 1), new Item("Item 2", 2), new Item("Item 3", 3) };
        }
    }

}