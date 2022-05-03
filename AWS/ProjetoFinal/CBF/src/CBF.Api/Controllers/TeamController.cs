using CBF.Application.Commands.Team;
using CBF.Application.Queries.Interfaces;
using CBF.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CBF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ITeamQueries _teamQueries;

        public TeamController(ITeamService teamService, ITeamQueries teamQueries)
        {
            _teamService = teamService;
            _teamQueries = teamQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _teamQueries.GetAll();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _teamQueries.GetById(id);
            return Ok(response);
        }
            
        [HttpPost]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> Post(CreateTeamCommand command)
        {
            var response = await _teamService.CreateTeam(command);
            if (response.Invalid)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{id:guid}/Players")]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> PostPlayers(Guid id, AddPlayersInTeamCommand command)
        {
            var response = await _teamService.AddPlayersTeam(id, command);
            if (response.Invalid)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
