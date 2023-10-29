using CurrentAccount.Domain.AccountsStatements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrentAccount.Data.Configs
{
    public class AccountStatementConfig
    {
        public void Configure(EntityTypeBuilder<AccountStatement> builder) 
        {
            builder.ToTable("AccountStatement");

            builder.HasKey(x => x.Id)
                .HasName("PK_AccountStatement");

            builder.Property(x => x.Id)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(x => x.Description)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired();

            builder.Property(x => x.Detached)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();
        }
    }
}
