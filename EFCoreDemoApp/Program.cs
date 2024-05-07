using EFCoreDemoApp.DataAccess;
using EFCoreDemoApp.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext with the DI container
builder.Services
    .AddDbContext<ShopDbContext>(opts =>
        {
            var cs = builder.Configuration.GetConnectionString("dbConnectionString");
            string? defaultSchema = builder.Configuration["DefaultSchema"];

            opts.UseSqlServer(cs, action => action.MigrationsHistoryTable(HistoryRepository.DefaultTableName, defaultSchema));

#if DEBUG
            opts.EnableDetailedErrors();
            opts.EnableSensitiveDataLogging();
#endif

            opts.ReplaceService<IMigrationsAssembly, SchemaAwareMigrationAssembly>();
        }, ServiceLifetime.Scoped)
    .AddScoped<IDefaultSchema>(sp =>
    {
        string? defaultSchema = builder.Configuration["DefaultSchema"];
        return new DbContextSchema(defaultSchema);
    });

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    using (var dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>())
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
