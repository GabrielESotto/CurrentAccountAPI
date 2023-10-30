using Bogus;
using CurrentAccount.Domain.AccountsStatements;
using FluentAssertions;
using Xunit;

namespace CurrentAccount.Domain.xTest.AccountsStatements
{
    public class AccountStatementTests
    {
        private readonly string _fakerLocale = "pt_BR";

        [Fact(DisplayName = "Add new account statement")]
        [Trait("Category", "Add")]
        public void AccountStatement_Add_ShouldGenerateNewInstance()
        {
            var description = GenerateWordFake();
            var value = GenerateValueFake();
            var date = DateTime.Now;
            var detached = GenerateAccountStatementAvulsoFake();
            var status = GenerateAccountStatementStatusFake();

            var accountStatement = AccountStatement.Add(description, date, value, detached, status);

            accountStatement.Description.Should().Be(description);
            accountStatement.Value.Should().Be(value);
            accountStatement.Date.Should().Be(date);
            accountStatement.Detached.Should().Be(detached);
            accountStatement.Status.Should().Be(status);
        }

        [Fact(DisplayName = "Update date and value from account statement")]
        [Trait("Category", "Update")]
        public void AccountStatement_Update_ShouldUpdateProperties()
        {
            var newValue = GenerateValueFake();
            var newDate = DateTime.Now.AddDays(1);
            var accountStatement = GenerateAccountStatementFake();

            accountStatement.Update(newDate, newValue);

            accountStatement.Value.Should().Be(newValue);
            accountStatement.Date.Should().Be(newDate);
        }

        [Fact(DisplayName = "Update status from account statement to Cancelado")]
        [Trait("Category", "Update")]
        public void AccountStatement_Cancel_ShouldUpdateStatusProperty()
        {
            var accountStatement = GenerateAccountStatementFake();

            accountStatement.Status.Should().Be(AccountStatementStatus.Valido);

            accountStatement.Cancel();

            accountStatement.Status.Should().Be(AccountStatementStatus.Cancelado);
        }
    
        private AccountStatement GenerateAccountStatementFake()
        {
            return AccountStatement.Add(GenerateWordFake(), DateTime.Now, GenerateValueFake(), GenerateAccountStatementAvulsoFake(), GenerateAccountStatementStatusFake());
        }

        private string GenerateWordFake()
        {
            return new Faker(_fakerLocale).Lorem.Word();
        }

        private decimal GenerateValueFake()
        {
            return new Faker(_fakerLocale).Random.Decimal();
        }

        private static AccountStatementAvulso GenerateAccountStatementAvulsoFake()
        {
            return AccountStatementAvulso.Avulso;
        }

        private static AccountStatementStatus GenerateAccountStatementStatusFake()
        {
            return AccountStatementStatus.Valido;
        }
    }
}
