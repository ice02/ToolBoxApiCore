﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ToolboxApi.Apps.V0.Model;
using ToolboxApi.Common;

namespace ToolboxApi.Apps.V0
{
    [ApiVersion("0.1", Deprecated = true)]
    [Produces("application/json")]
    [ApiController]
    public class AppsController : ApiControllerBase
    {
        [HttpGet]
        public IEnumerable<Item> GetAsync()
        {
            //var container = new Container(new Uri("http://localhost:58200/odata/"));

            //var response = await container.Clients.ExecuteAsync();

            //var query = (DataServiceQuery)container
            //    .Clients
            //    .Expand("Address")
            //    .Where(it => it.Id > 10);

            //var resp2 = await query.ExecuteAsync();
            //foreach (Client item in resp2)
            //{
            //    Console.WriteLine(item.Address.City);
            //}

            //foreach (Client item in response)
            //{
            //    Console.WriteLine(item.Name);
            //}

            //var c = container.Clients.ByKey(100);
            //var s = await c.NameLength().GetValueAsync();
            //Console.WriteLine(s);

            return new[] { new Item("Item 1", 1), new Item("Item 2", 2), new Item("Item 3", 3) };
        }
    }
}
