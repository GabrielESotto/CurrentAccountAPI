using Nuuvify.CommonPack.Domain;
using System.Text.Json.Serialization;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Inputs
{
    public class AccountStatementUpdateCommand : ICommandR
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public bool SaveChanges { get; set; } = true;
        [JsonIgnore]
        public bool RemoveNotificationsBeginning { get; set; }
    }
}
