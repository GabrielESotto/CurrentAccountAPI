using AutoMapper;
using CurrentAccount.Domain.AccountsStatements;
using CurrentAccount.Domain.AccountsStatements.Commands.Inputs;
using CurrentAccount.Domain.AccountsStatements.Commands.Results;

namespace CurrentAccount.Dto
{
    public class AccountStatementMapProfile : Profile
    {
        public AccountStatementMapProfile()
        {
            CreateMap<AccountStatementAddCommand, AccountStatementQueryResult>().ReverseMap();
            CreateMap<AccountStatementUpdateCommand, AccountStatementQueryResult>().ReverseMap();
            CreateMap<AccountStatementCancelCommand, AccountStatementQueryResult>().ReverseMap();
            CreateMap<AccountStatement, AccountStatementQueryResult>().ReverseMap();
        }
    }
}
