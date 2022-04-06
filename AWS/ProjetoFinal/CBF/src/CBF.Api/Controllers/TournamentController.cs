using CBF.Application.Commands.Tournament;
using CBF.Application.InternalEvent;
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
        private readonly ITournamentService _tournamentService;
        private readonly IBus _bus;

        public TournamentController(ITournamentService tournamentService, IBus bus)
        {
            _tournamentService = tournamentService;
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTournamentCommand command)
        {
            var response = await _tournamentService.CreateTournament(command);
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
            var response = await _tournamentService.AddMatch(command);
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
            var evt = new CreateEventInternalEvent { TournamentId = id ,MatchId = matchId, Message = message };
            await _bus.Publish(evt);

            return Ok();
        }
    }
}
