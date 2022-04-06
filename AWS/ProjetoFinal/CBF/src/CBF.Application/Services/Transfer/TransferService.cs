using CBF.Application.Commands;
using CBF.Application.Commands.Transfer;
using CBF.Application.Services.Interfaces;
using MediatR;
using System.Threading.Tasks;
using DomainTransfer = CBF.Domain.Transfer;

namespace CBF.Application.Services.Transfer
{
    public class TransferService : ITransferService
    {
        private readonly IMediator _mediator;

        public TransferService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CommandResponse<DomainTransfer>> CreateTransfer(CreateTransferCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResponse<DomainTransfer>(null, command.Notifications);
            }

            var result = await _mediator.Send(command);

            return new CommandResponse<DomainTransfer>(result.Data, result.Notifications);
        }
    }
}
