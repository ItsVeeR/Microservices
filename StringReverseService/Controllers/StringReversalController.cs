using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using StringReverseService.Hubs;
using StringReverseService.Models;
using StringReverseService.Repository;
using System;
using System.Net;
using System.Net.Http;
using System.Transactions;

namespace StringReverseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringReversalController : ControllerBase
    {
        private readonly IInputStringRepository inputStringRepository;
        private readonly ILogger logger;
        //private readonly IHubContext<NotificationsHub> hubContext;
        IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();
         
        public StringReversalController(IInputStringRepository inputStringRepository, 
                                        ILogger<StringReversalController> logger)
        {
            this.logger = logger;
            this.inputStringRepository = inputStringRepository; 
        }
         
        [HttpGet("GetAll")]
        public IActionResult ListAll()
        {
            var allInputs = inputStringRepository.ListAllInputs();
            return new OkObjectResult(allInputs);
        }

         
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByRequestId(Guid? id)
        {
            if(id == null || !id.HasValue)
            {
                this.logger.LogError($"Invalid id in GetByRequestId Request at {DateTime.UtcNow}.");
                return BadRequest();
            }
            var result = inputStringRepository.GetByID(id.Value);

            if(result == null)
            {
                this.logger.LogWarning($"No result found for id {id.Value} .");
            }

            return new OkObjectResult(result);
        }

        //POST: api/ReverseString
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        public IActionResult ReverseString([FromBody] InputString value)
        {
            if (String.IsNullOrWhiteSpace(value.InputValue))
            {
                return BadRequest("Incorrect Input");
            }

            using (var scope = new TransactionScope())
            {
                inputStringRepository.InsertInputString(value);
                scope.Complete();
                var reversed = Business.Reverse(value.InputValue); 

                hub.Clients.All.NotifyAllClients();
                return new OkObjectResult(reversed); 
            }
        } 
    }
}
