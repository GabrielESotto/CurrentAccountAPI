using Nuuvify.CommonPack.Domain;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Results
{
    public class AccountStatementQueryResult : ICommandResultR
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public AccountStatementAvulso Detached { get; set; }
        public AccountStatementStatus Status { get; set; }
    }
}
