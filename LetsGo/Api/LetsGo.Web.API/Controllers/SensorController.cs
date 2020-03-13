using System;
using System.Threading.Tasks;
using LetsGo.Web.API.Models.Requests;
using LetsGo.Web.API.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LetsGo.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {

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
