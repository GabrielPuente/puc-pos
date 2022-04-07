using CBF.Application.Commands.Tournament;
using CBF.Application.InternalEvent;
using CBF.Application.Queries.Interfaces;
using CBF.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using System;
using System.Threading.Tasks;

namespace CBF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _service;
        private readonly ITournamentQueries _queries;
        private readonly IBus _bus;

        public TournamentController(ITournamentService service, IBus bus, ITournamentQueries queries)
        {
            _service = service;
            _bus = bus;
            _queries = queries;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTournamentCommand command)
        {
            var response = await _service.CreateTournament(command);
            if (response.Invalid)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("{id:guid}/Match")]
        public async Task<IActionResult> Post(Guid id, AddMatchCommand command)
        {
            command.TournamentId = id;
            var response = await _service.AddMatch(command);
            if (response.Invalid)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("{id:guid}/Match/{matchId:guid}/event")]
        [AllowAnonymous]
        public async Task<IActionResult> PostEvent(Guid id, Guid matchId, [FromBody] string message)
        {
            var evt = new CreateEventInternalEvent { TournamentId = id, MatchId = matchId, Message = message };
            await _bus.Publish(evt);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _queries.GetAll();
            return Ok(response);
        }

        [HttpGet("{id:guid}/matchs")]
        public async Task<IActionResult> GetAllMatchByTournamentId(Guid id)
        {
            var response = await _queries.GetAllMatchByTournamentId(id);
            return Ok(response);
        }

        [HttpGet("{id:guid}/match/{matchId}/events")]
        public async Task<IActionResult> GetAllEventMatchByTournamentId(Guid id, Guid matchId)
        {
            var response = await _queries.GetAllEventMatchByTournamentId(id, matchId);
            return Ok(response);
        }
    }
}
