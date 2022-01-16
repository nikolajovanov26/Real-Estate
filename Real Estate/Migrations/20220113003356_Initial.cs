using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Real_Estate.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DatumOsnovanje = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Provizija = table.Column<int>(type: "int", nullable: true),
                    Prodadeni = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencija", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nedviznosti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Golemina = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Kategorija = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    AgencijaId = table.Column<int>(type: "int", nullable: true),
                    BrojOmileni = table.Column<int>(type: "int", nullable: true),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nedviznosti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nedviznosti_Agencija_AgencijaId",
                        column: x => x.AgencijaId,
                        principalTable: "Agencija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nedviznosti_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Omileni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(type: "int", nullable: true),
                    NedviznostiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Omileni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Omileni_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Omileni_Nedviznosti_NedviznostiId",
                        column: x => x.NedviznostiId,
                        principalTable: "Nedviznosti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nedviznosti_AgencijaId",
                table: "Nedviznosti",
                column: "AgencijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Nedviznosti_KorisnikId",
                table: "Nedviznosti",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Omileni_KorisnikId",
                table: "Omileni",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Omileni_NedviznostiId",
                table: "Omileni",
                column: "NedviznostiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Omileni");

            migrationBuilder.DropTable(
                name: "Nedviznosti");

            migrationBuilder.DropTable(
                name: "Agencija");

            migrationBuilder.DropTable(
                name: "Korisnik");
        }
    }
}
