using CBF.Application.Commands.Player;
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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerQueries _queries;
        private readonly IPlayerService _service;

        public PlayerController(IPlayerQueries queries, IPlayerService service)
        {
            _queries = queries;
            _service = service;
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, UpdatePlayerMarketValueCommand command)
        {
            var response = await _service.UpdatePlayerMarketValue(id, command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _queries.GetAll();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _queries.GetById(id);
            return Ok(response);
        }
    }
}
