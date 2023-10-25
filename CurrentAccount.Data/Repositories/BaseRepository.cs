using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nuuvify.CommonPack.Extensions.Notificator;
using Nuuvify.CommonPack.UnitOfWork;
using Nuuvify.CommonPack.UnitOfWork.Abstraction.Interfaces;
using System.Data;

namespace CurrentAccount.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : Repository<TEntity> where TEntity : class
    {
        protected string ownerDB;
        protected readonly IMapper _mapper;
        protected IDbConnection cn;
        protected BaseRepository(DbContext dbContext,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(dbContext, unitOfWork)
        {
            _mapper = mapper;

            dbContext.SetDbContextUsername(unitOfWork.UsernameContext, unitOfWork.UserIdContext);
        }

        public virtual void SetDbConnection(IDbConnection dbConnection, string cnnString)
        {
            if (!(dbConnection == null))
                cn = dbConnection;


            if (string.IsNullOrWhiteSpace(cn?.ConnectionString))
                cn.ConnectionString = cnnString;
        }

        public virtual IList<NotificationR> ValidationResult()
        {
            return GetNotifications();
        }
    }
}
