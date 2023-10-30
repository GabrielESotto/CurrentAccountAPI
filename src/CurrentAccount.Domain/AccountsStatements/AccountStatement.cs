namespace CurrentAccount.Domain.AccountsStatements
{
    public class AccountStatement : Entity
    {
        public AccountStatement() { }

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

        public void Update(DateTime date, decimal value)
        {
            Date = date;
            Value = value;
        }

        public void Cancel()
        {
            Status = AccountStatementStatus.Cancelado;
        }

    }
}
