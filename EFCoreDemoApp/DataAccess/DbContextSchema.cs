using EFCoreDemoApp.Integration;

namespace EFCoreDemoApp.DataAccess
{
    public class DbContextSchema : IDefaultSchema
    {
        public DbContextSchema(string? defaultSchema)
        {
            DefaultSchema = defaultSchema ?? throw new ArgumentNullException(nameof(defaultSchema));
        }

        public string DefaultSchema { get; }
    }
}
