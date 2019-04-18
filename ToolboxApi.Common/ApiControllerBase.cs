using Microsoft.AspNetCore.Mvc;
using System;

namespace ToolboxApi.Common
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
