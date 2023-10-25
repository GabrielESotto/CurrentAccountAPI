using Nuuvify.CommonPack.Domain;

namespace CurrentAccount.Domain.AccountsStatements
{
    public class AccountStatement : DomainEntity
    {
        protected AccountStatement() { }

        private AccountStatement(string description, DateTime date, decimal value, AccountStatementAvulso detached, AccountStatementStatus status)
        {
            Description = description;
            Date = date;
            Value = value;
            Detached = detached;
            Status = status;
        }

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public AccountStatementAvulso Detached { get; set; }
        public AccountStatementStatus Status { get; set; }

        public static AccountStatement Add(string description, DateTime date, decimal value, AccountStatementAvulso detached, AccountStatementStatus status)
        {
            return new AccountStatement(description, date, value, detached, status);
        }

        public void Update(string description, DateTime date, decimal value, AccountStatementAvulso detached, AccountStatementStatus status)
        {
            Description = description;
            Date = date; 
            Value = value; 
            Detached = detached; 
            Status = status;
        }
    
    }
}
