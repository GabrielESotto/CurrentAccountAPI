using CurrentAccount.Domain.AccountsStatements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Nuuvify.CommonPack.Middleware.Abstraction;
using Nuuvify.CommonPack.UnitOfWork;
using System.Diagnostics;
using System.Text;

namespace CurrentAccount.Data.Context
{
    public class SqlDbContext : DbContext
    {
        public readonly string ownerDB;
        public readonly IConfigurationCustom Configuration;

        public SqlDbContext(IConfigurationCustom configuration)
        {
            Configuration = configuration;
            ownerDB = Configuration.GetSectionValue("AppConfig:OwnerDB:Schema");
        }

        public SqlDbContext(DbContextOptions<SqlDbContext> options, IConfigurationCustom configuration) : base(options) 
        {
            Configuration = configuration;
            ownerDB = Configuration.GetSectionValue("AppConfig:OwnerDB:Schema");
        }

        public DbSet<AccountStatement> AccountsStatements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                var cnn = Configuration.GetSectionValue("AppConfig:OwnerDB:Schema");

                optionsBuilder.UseSqlServer(Configuration.GetConnectionString(cnn))
                    .UseLazyLoadingProxies()
                    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetDatabaseProviderName(Database);

            modelBuilder.HasDefaultSchema(ownerDB);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlDbContext).Assembly,
                predicate: n => n.Namespace!.EndsWith(nameof(SqlDbContext)));

            modelBuilder.IgnoreValueObject();
            modelBuilder.MappingPropertiesForgotten();

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var registries = await SaveChangesAsync(true, cancellationToken);
                Debug.WriteLine($"SaveChanges executado com sucesso para {registries} registros, e {this.GetAggregatesChanges()} registros em entidades agregadas");
                return await Task.FromResult(registries);
            }
            catch (DbUpdateException ex)
            {
                PropertyValues proposedValues;
                PropertyValues databaseValues;

                var columnName = string.Empty;
                var baseMessage = new StringBuilder()
                    .AppendLine($"Houve um erro em SaveChanges, verifique o log de erro: {ex.Message} inner: {ex.InnerException?.Message}");

                foreach (var entry in ex.Entries)
                {
                    proposedValues = entry.CurrentValues;
                    databaseValues = entry.GetDatabaseValues();

                    foreach (var property in proposedValues.Properties)
                    {
                        columnName = property.GetColumnName();
                        baseMessage.AppendLine($"Proposed: {columnName} = {proposedValues[property]}");
                        if (!(databaseValues?[property] is null))
                        {
                            baseMessage.AppendLine($"Database Value: {columnName} = {databaseValues?[property]}");
                        }
                    }
                }

                Debug.WriteLine($"{baseMessage}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message} inner: {ex.InnerException?.Message}");
                throw;
            }
        }
    }
}
