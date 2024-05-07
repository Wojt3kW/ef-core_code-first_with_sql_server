using EFCoreDemoApp.Integration;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreDemoApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        private readonly string _schema;

        public InitialMigration(IDefaultSchema schema)
        {
            _schema = schema?.DefaultSchema ?? throw new ArgumentNullException(nameof(schema));
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                schema: _schema,
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Line1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address_Line2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address_PostCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: _schema,
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contents = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BillingAddress_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BillingAddress_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BillingAddress_Line1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BillingAddress_Line2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BillingAddress_PostCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress_Line1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ShippingAddress_Line2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ShippingAddress_PostCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalSchema: _schema,
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                schema: _schema,
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                schema: _schema,
                name: "Orders");

            migrationBuilder.DropTable(
                schema: _schema,
                name: "Customers");
        }
    }
}
