using AutoMapper;
using CurrentAccount.Domain.AccountsStatements.Commands.Inputs;
using CurrentAccount.Domain.AccountsStatements.Interfaces;
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

        public AccountsStatementsController(IAccountStatementRepository accountStatementRepository,
            IAccountStatement accountStatement,
            IMapper mapper)
        {
            _accountStatementRepository = accountStatementRepository;
            _accountStatement = accountStatement;
            _mapper = mapper;
        }

        [HttpPost, MapToApiVersion("1.0")]
        public async Task<IActionResult> Add([FromBody] AccountStatementAddCommand command, CancellationToken cancellationToken)
        {
            var result = await _accountStatement.Handle(command, cancellationToken);

            return await Response(new StatusCodeResult(StatusCodes.Status200OK),
                new StatusCodeResult(StatusCodes.Status422UnprocessableEntity), result, _accountStatement.ValidationResult());
        }
    }
}
