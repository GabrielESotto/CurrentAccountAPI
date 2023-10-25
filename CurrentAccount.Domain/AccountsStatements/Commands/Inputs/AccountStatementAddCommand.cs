using Nuuvify.CommonPack.Domain;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Inputs
{
    public class AccountStatementAddCommand : ICommandR
    {
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public AccountStatementAvulso Detached { get; set; }
        public AccountStatementStatus Status { get; set; }
        public bool SaveChanges { get; set; } = true;
        public bool RemoveNotificationsBeginning { get; set; }
    }
}
