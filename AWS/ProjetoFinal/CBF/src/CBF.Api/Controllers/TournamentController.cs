using CBF.Application.Commands.Match;
using CBF.Application.Commands.Tournament;
using CBF.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CBF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly IMatchService _matchService;

        public TournamentController(ITournamentService tournamentService, IMatchService matchService)
        {
            _tournamentService = tournamentService;
            _matchService = matchService;
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

        [HttpPost("Match")]
        public async Task<IActionResult> Post(CreateMatchCommand command)
        {
            var response = await _matchService.CreateMatch(command);
            if (response.Invalid)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
