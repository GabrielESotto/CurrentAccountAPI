using Nuuvify.CommonPack.Domain;
using System.Text.Json.Serialization;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Inputs
{
    public class AccountStatementAddCommand : ICommandR
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public AccountStatementAvulso Detached { get; set; }
        public AccountStatementStatus Status { get; set; }
        [JsonIgnore]
        public bool SaveChanges { get; set; } = true;
        [JsonIgnore]
        public bool RemoveNotificationsBeginning { get; set; }
    }
}
