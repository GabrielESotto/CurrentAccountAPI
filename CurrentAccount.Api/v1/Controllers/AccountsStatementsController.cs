using AutoMapper;
using CurrentAccount.Domain.AccountsStatements.Commands.Inputs;
using CurrentAccount.Domain.AccountsStatements.Commands.Results;
using CurrentAccount.Domain.AccountsStatements.Interfaces;
using CurrentAccount.Domain.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrentAccount.Api.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AccountsStatementsController : BaseController
    {
        private readonly IAccountStatementRepository _accountStatementRepository;
        private readonly IAccountStatement _accountStatement;
        private readonly IMapper _mapper;
        private readonly INotification _notification;

        public AccountsStatementsController(IAccountStatementRepository accountStatementRepository,
            IAccountStatement accountStatement,
            INotification notification,
            IMapper mapper)
            : base(notification)
        {
            _accountStatementRepository = accountStatementRepository;
            _accountStatement = accountStatement;
            _mapper = mapper;
            _notification = notification;
        }

        [HttpGet, MapToApiVersion("1.0")]
        public async Task<ActionResult> GetAll()
        {
            var accountStatements = _mapper.Map<IEnumerable<AccountStatementQueryResult>>(await _accountStatementRepository.GetAll());
            return CustomResponse(accountStatements);
        }

        [HttpGet("{id:guid}"), MapToApiVersion("1.0")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var accountStatement = _mapper.Map<AccountStatementQueryResult>(await _accountStatementRepository.GetById(id));

            if (accountStatement == null) return NotFound();

            return CustomResponse(accountStatement);
        }

        [HttpPost, MapToApiVersion("1.0")]
        public async Task<ActionResult<AccountStatementQueryResult>> Add(AccountStatementAddCommand command, CancellationToken cancellationToken)
        {
            var result = await _accountStatement.Handle(command, cancellationToken);
            return CustomResponse(result);
        }

        [HttpPut, MapToApiVersion("1.0")]
        public async Task<ActionResult<AccountStatementQueryResult>> Update(AccountStatementUpdateCommand command, CancellationToken cancellationToken)
        {
            var result = await _accountStatement.Handle(command, cancellationToken);
            return CustomResponse(result);
        }

        [HttpPut("Cancel"), MapToApiVersion("1.0")]
        public async Task<ActionResult<AccountStatementQueryResult>> Cancel([FromQuery] AccountStatementCancelCommand command, CancellationToken cancellationToken)
        {
            var result = await _accountStatement.Handle(command, cancellationToken);
            return CustomResponse(result);
        }

        [HttpPost("NotDetached"), MapToApiVersion("1.0")]
        public async Task<ActionResult<AccountStatementQueryResult>> AddNotDetached(AccountStatementAddNotDetachedCommand command, CancellationToken cancellationToken)
        {
            var result = await _accountStatement.Handle(command, cancellationToken);
            return CustomResponse(result);
        }
    }
}
