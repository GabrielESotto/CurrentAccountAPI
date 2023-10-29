using CurrentAccount.Domain.AccountsStatements.Commands.Inputs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Validators
{
    public class AccountStatementUpdateCommandValidator : AbstractValidator<AccountStatementUpdateCommand>
    {
        public AccountStatementUpdateCommandValidator() 
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Informe a transação a ser atualizada");

            RuleFor(c => c.Value)
                .NotEmpty()
                .WithMessage("Valor deve ser preenchido");

            RuleFor(c => c.Date)
                .NotEmpty()
                .WithMessage("Data deve ser preenchido");
        }
    }
}
