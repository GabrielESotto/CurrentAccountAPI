using AutoMapper;
using CurrentAccount.Domain.AccountsStatements.Commands.Inputs;
using CurrentAccount.Domain.AccountsStatements.Interfaces;
using MediatR;
using Nuuvify.CommonPack.Domain;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Command
{
    public class AccountStatementCommandHandler : BaseDomain, IAccountStatement
    {
        private readonly IAccountStatementRepository _repository;

        public AccountStatementCommandHandler(IAccountStatementRepository repository,
            IMediator mediator,
            IMapper mapper)
            : base(mediator, mapper)
        {
            _repository = repository;
        }

        public async Task<ICommandResultR> Handle(AccountStatementAddCommand request, CancellationToken cancellationToken)
        {
            return null;
        }

        public async Task<ICommandResultR> Handle(AccountStatementUpdateCommand request, CancellationToken cancellationToken)
        {
            return null;
        }

        public async Task<ICommandResultR> Handle(AccountStatementDeleteCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
