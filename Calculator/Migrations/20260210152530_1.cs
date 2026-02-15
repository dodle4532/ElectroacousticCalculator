using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calculator.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    H = table.Column<double>(type: "REAL", nullable: false),
                    UH = table.Column<double>(type: "REAL", nullable: false),
                    U_vh = table.Column<double>(type: "REAL", nullable: false),
                    delta = table.Column<double>(type: "REAL", nullable: false),
                    USH_p = table.Column<double>(type: "REAL", nullable: false),
                    a = table.Column<double>(type: "REAL", nullable: false),
                    b_ = table.Column<double>(type: "REAL", nullable: false),
                    h_ = table.Column<double>(type: "REAL", nullable: false),
                    N = table.Column<double>(type: "REAL", nullable: false),
                    a1 = table.Column<double>(type: "REAL", nullable: false),
                    a2 = table.Column<double>(type: "REAL", nullable: false),
                    V = table.Column<double>(type: "REAL", nullable: false),
                    S = table.Column<double>(type: "REAL", nullable: false),
                    a_ekv = table.Column<double>(type: "REAL", nullable: false),
                    S_sr = table.Column<double>(type: "REAL", nullable: false),
                    B = table.Column<double>(type: "REAL", nullable: false),
                    t_r = table.Column<double>(type: "REAL", nullable: false),
                    SpeakerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpeakerType = table.Column<int>(type: "INTEGER", nullable: false),
                    SpeakerTyp = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    P_Vt = table.Column<double>(type: "REAL", nullable: false),
                    P_01 = table.Column<double>(type: "REAL", nullable: false),
                    SHDN_v = table.Column<double>(type: "REAL", nullable: false),
                    SHDN_g = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calculations");

            migrationBuilder.DropTable(
                name: "Speakers");
        }
    }
}
