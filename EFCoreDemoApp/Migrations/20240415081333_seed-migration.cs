using EFCoreDemoApp.Integration;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreDemoApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedMigration : Migration
    {
        private readonly string _schema;

        public SeedMigration(IDefaultSchema schema)
        {
            _schema = schema?.DefaultSchema ?? throw new ArgumentNullException(nameof(schema));
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: _schema,
                table: "Customers",
                columns: new[] { "Id", "Name", "Address_City", "Address_Country", "Address_Line1", "Address_PostCode" },
                columnTypes: new string[] { "int", "string", "string", "string", "string", "string" },
                values: new object[] { 1, "John Doe", "London", "UK", "123 High Street", "SW1A 1AA" });

            migrationBuilder.InsertData(
                schema: _schema,
                table: "Orders",
                columns: new[] { "Id", "Contents", "CustomerId", "BillingAddress_City", "BillingAddress_Country", "BillingAddress_Line1", "BillingAddress_PostCode", "ShippingAddress_City", "ShippingAddress_Country", "ShippingAddress_Line1", "ShippingAddress_PostCode" },
                columnTypes: new string[] { "int", "string", "int", "string", "string", "string", "string", "string", "string", "string", "string" },
                values: new object[] { 1, "1 x Widget", 1, "London", "UK", "124 High Street", "SW1A 1AA", "London", "UK", "125 High Street", "SW1A 1AA" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
