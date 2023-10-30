using Nuuvify.CommonPack.Domain;
using System.Text.Json.Serialization;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Inputs
{
    public class AccountStatementAddNotDetachedCommand : ICommandR
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        [JsonIgnore]
        public AccountStatementAvulso Detached { get; set; } = AccountStatementAvulso.NaoAvulso;
        [JsonIgnore]
        public AccountStatementStatus Status { get; set; } = AccountStatementStatus.Valido;
        [JsonIgnore]
        public bool SaveChanges { get; set; } = true;
        [JsonIgnore]
        public bool RemoveNotificationsBeginning { get; set; }
    }
}

