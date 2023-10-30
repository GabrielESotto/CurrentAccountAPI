using CurrentAccount.Domain.AccountsStatements.Commands.Inputs;
using FluentValidation;

namespace CurrentAccount.Domain.AccountsStatements.Commands.Validators
{
    public class AccountStatementAddCommandValidator : AbstractValidator<AccountStatementAddCommand>
    {
        public AccountStatementAddCommandValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Descrição da transação deve ser preenchido");

            RuleFor(c => c.Value)
                .NotEmpty()
                .WithMessage("Valor deve ser preenchido");
            
            RuleFor(c => c.Detached)
                .IsInEnum()
                .WithMessage("Informe se a transação é Avulso ou Não Avulso");

            RuleFor(c => c.Status)
                .IsInEnum()
                .WithMessage("Informe se o status da transação é Válido ou Não Válido");

        }
    }
}
