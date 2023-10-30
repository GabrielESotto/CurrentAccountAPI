using AutoMapper;
using CurrentAccount.Domain.AccountsStatements.Commands.Inputs;
using CurrentAccount.Domain.AccountsStatements.Commands.Results;
using CurrentAccount.Domain.AccountsStatements.Commands.Validators;
using CurrentAccount.Domain.AccountsStatements.Interfaces;
using CurrentAccount.Domain.Notifications;
using CurrentAccount.Domain.Notifications.Interfaces;
using Nuuvify.CommonPack.Domain;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Command
{
    public class AccountStatementCommandHandler : BaseDomain, IAccountStatement
    {
        private readonly IAccountStatementRepository _repository;
        private readonly INotification _notification;

        public AccountStatementCommandHandler(IAccountStatementRepository repository,
            INotification notification,
            MediatR.IMediator mediator,
            IMapper mapper)
            : base(mediator, mapper)
        {
            _notification = notification;
            _repository = repository;
        }

        public async Task<ICommandResultR> Handle(AccountStatementAddCommand request, CancellationToken cancellationToken)
        {
            if(!IsAddCommandValid(request)) return null;

            var accountStatement = AccountStatement.Add(request.Description, DateTime.Now, request.Value, request.Detached, request.Status);

            await _repository.Add(accountStatement);

            return _mapper.Map<AccountStatementQueryResult>(accountStatement);
        }

        public async Task<ICommandResultR> Handle(AccountStatementAddNotDetachedCommand request, CancellationToken cancellationToken)
        {
            var accountStatement = AccountStatement.Add(request.Description, DateTime.Now, request.Value, request.Detached, request.Status);

            await _repository.Add(accountStatement);

            return _mapper.Map<AccountStatementQueryResult>(accountStatement);
        }

        public async Task<ICommandResultR> Handle(AccountStatementUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!IsUpdateCommandValid(request)) return null;

            var accountStatementToBeUpdated = await _repository.GetById(request.Id);

            if (accountStatementToBeUpdated == null) 
            {
                _notification.Handle(new Notification("Parametros estão nulos"));
                return null;
            }

            if (accountStatementToBeUpdated.Detached == AccountStatementAvulso.NaoAvulso || accountStatementToBeUpdated.Status == AccountStatementStatus.Cancelado) 
            {
                _notification.Handle(new Notification("Impossível alterar um lançamento não avulso ou cancelado."));
                return null;
            }

            accountStatementToBeUpdated.Update(request.Date, request.Value);

            await _repository.Update(accountStatementToBeUpdated);

            return _mapper.Map<AccountStatementQueryResult>(accountStatementToBeUpdated);
        }

        public async Task<ICommandResultR> Handle(AccountStatementCancelCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _notification.Handle(new Notification("Parametros estão nulos"));
                return null;
            }

            var accountStatementToBeCanceled = await _repository.GetById(request.Id);

            if (accountStatementToBeCanceled == null)
            {
                _notification.Handle(new Notification("Esse extrato não existe"));
                return null;
            }

            if (accountStatementToBeCanceled.Detached == AccountStatementAvulso.NaoAvulso || accountStatementToBeCanceled.Status == AccountStatementStatus.Cancelado)
            {
                _notification.Handle(new Notification("Impossível cancelar um lançamento não avulso ou já cancelado."));
                return null;
            }

            accountStatementToBeCanceled.Cancel();
            await _repository.Update(accountStatementToBeCanceled);

            return _mapper.Map<AccountStatementQueryResult>(accountStatementToBeCanceled);
        }

        private bool IsAddCommandValid(AccountStatementAddCommand request)
        {
            if (request == null)
            {
                _notification.Handle(new Notification("Parametros estão nulos"));
                return false;
            }

            var validator = new AccountStatementAddCommandValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                    _notification.Handle(new Notification(failure.ErrorMessage));

                return false;
            }

            return true;
        }

        private bool IsUpdateCommandValid(AccountStatementUpdateCommand request)
        {
            if (request == null)
            {
                _notification.Handle(new Notification("Parametros estão nulos"));
                return false;
            }

            var validator = new AccountStatementUpdateCommandValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                    _notification.Handle(new Notification(failure.ErrorMessage));

                return false;
            }

            return true;
        }
    }
}
