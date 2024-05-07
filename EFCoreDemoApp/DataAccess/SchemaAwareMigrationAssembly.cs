using EFCoreDemoApp.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using System.Reflection;

namespace EFCoreDemoApp.DataAccess
{
    public class SchemaAwareMigrationAssembly : MigrationsAssembly
    {
        private readonly DbContext _context;

        public SchemaAwareMigrationAssembly(
            ICurrentDbContext currentContext,
            IDbContextOptions options,
            IMigrationsIdGenerator idGenerator,
            IDiagnosticsLogger<DbLoggerCategory.Migrations> logger)
            : base(currentContext, options, idGenerator, logger)
        {
            _context = currentContext.Context;
        }

        public override Migration CreateMigration(TypeInfo migrationClass, string activeProvider)
        {
            if (activeProvider is null)
            {
                throw new ArgumentNullException(nameof(activeProvider));
            }

            var hasConstructorWithSchema = migrationClass.GetConstructor(new[] { typeof(IDefaultSchema) }) is not null;

            if (hasConstructorWithSchema && _context is IDefaultSchema schema)
            {
                var instance = Activator.CreateInstance(migrationClass.AsType(), schema) as Migration;
                instance!.ActiveProvider = activeProvider;
                return instance;
            }

            return base.CreateMigration(migrationClass, activeProvider);
        }
    }
}
