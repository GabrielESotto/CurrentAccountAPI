using Nuuvify.CommonPack.Domain;
using System.Text.Json.Serialization;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Inputs
{
    public class AccountStatementCancelCommand : ICommandR
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public bool SaveChanges { get; set; } = true;
        [JsonIgnore]
        public bool RemoveNotificationsBeginning { get; set; }
    }
}
