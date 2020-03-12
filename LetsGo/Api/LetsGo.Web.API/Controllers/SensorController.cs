using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LetsGo.Web.API.Models.Requests;
using LetsGo.Web.API.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LetsGo.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ILogger<SensorController> _logger;

        public SensorController(ILogger<SensorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{idPinButton}")]
        public async Task<ActionResult<GetStateResponse>> GetState(string idPinButton)
        {
            return new GetStateResponse { IdPinButton = Convert.ToInt16(idPinButton), StateBook = 0 };
        }

        [HttpPost("UpdateState")]
        public async Task<ActionResult<int>> PostState(PostStateRequest postRequest)
        {
            return postRequest.IsAvailable;
        }
    }
}
