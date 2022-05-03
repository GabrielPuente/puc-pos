using CBF.Application.Commands.Transfer;
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
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;
        private readonly ITransferQueries _transferQueries;

        public TransferController(ITransferService transferService, ITransferQueries transferQueries)
        {
            _transferService = transferService;
            _transferQueries = transferQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _transferQueries.GetAll();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _transferQueries.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> Post(CreateTransferCommand command)
        {
            var response = await _transferService.CreateTransfer(command);
            if (response.Invalid)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
