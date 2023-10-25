using Nuuvify.CommonPack.Domain;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Inputs
{
    public class AccountStatementDeleteCommand : ICommandR
    {
        public Guid Id { get; set; }
        public bool SaveChanges { get; set; } = true;
        public bool RemoveNotificationsBeginning { get; set; }
    }
}
