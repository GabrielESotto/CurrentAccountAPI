using AutoMapper;
using CurrentAccount.Data.Context;
using CurrentAccount.Domain.AccountsStatements;
using CurrentAccount.Domain.AccountsStatements.Interfaces;
using Microsoft.EntityFrameworkCore;
using Nuuvify.CommonPack.UnitOfWork.Abstraction.Interfaces;

namespace CurrentAccount.Data.Repositories
{
    public class AccountStatementRepository : BaseRepository<AccountStatement>, IAccountStatementRepository
    {
        public AccountStatementRepository(SqlDbContext dbContext,
            IUnitOfWork<SqlDbContext> unitOfWork,
            IMapper mapper) : base(dbContext, unitOfWork, mapper) 
        {
            ownerDB = dbContext.ownerDB;
            var cnn = dbContext.Configuration.GetSectionValue("AppConfig:OwnerDB:Cnn");
            SetDbConnection(dbContext.Database.GetDbConnection(), dbContext.Configuration.GetConnectionString(cnn));
        }
    }
}
