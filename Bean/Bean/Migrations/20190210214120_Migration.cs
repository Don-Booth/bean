using Microsoft.EntityFrameworkCore.Migrations;

namespace Bean.Migrations
{
    public partial class Migration : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    QuoteId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuoteText = table.Column<string>(nullable: true),
                    QuoteAuthor = table.Column<string>(nullable: true),
                    QuoteSource = table.Column<string>(nullable: true),
                    QuoteDate = table.Column<string>(nullable: true),
                    QuoteContributor = table.Column<string>(nullable: true),
                    DateAdded = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.QuoteId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");
        }
    }
}
