using CurrentAccount.Data.Context;
using CurrentAccount.Domain.AccountsStatements;
using CurrentAccount.Domain.AccountsStatements.Interfaces;

namespace CurrentAccount.Data.Repositories
{
    public class AccountStatementRepository : BaseRepository<AccountStatement>, IAccountStatementRepository
    {
        public AccountStatementRepository(SqlDbContext context) : base(context) { }
    }
}
