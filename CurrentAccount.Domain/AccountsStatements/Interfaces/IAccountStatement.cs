using CurrentAccount.Domain.AccountsStatements.Commands.Inputs;
using MediatR;
using Nuuvify.CommonPack.Domain;
using Nuuvify.CommonPack.Domain.Interfaces;

namespace CurrentAccount.Domain.AccountsStatements.Interfaces
{
    public interface IAccountStatement : IBaseDomain,
        IRequestHandler<AccountStatementAddCommand, ICommandResultR>,
        IRequestHandler<AccountStatementUpdateCommand, ICommandResultR>,
        IRequestHandler<AccountStatementDeleteCommand, ICommandResultR>
    {
    }
}
