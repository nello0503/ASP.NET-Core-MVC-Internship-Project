using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Formazione.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corsi",
                columns: table => new
                {
                    TipoCorsoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescrizioneCodice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durata = table.Column<int>(type: "int", nullable: true),
                    Sedute = table.Column<int>(type: "int", nullable: true),
                    Moduli = table.Column<int>(type: "int", nullable: true),
                    AnniValidità = table.Column<int>(type: "int", nullable: true),
                    MaxDiscenti = table.Column<int>(type: "int", nullable: true),
                    dataorains = table.Column<DateTime>(type: "datetime", nullable: true),
                    IDutenteins = table.Column<int>(type: "int", nullable: true),
                    dataoraultmod = table.Column<DateTime>(type: "datetime", nullable: true),
                    IDutenteultmod = table.Column<int>(type: "int", nullable: true),
                    eliminato = table.Column<short>(type: "smallint", nullable: false),
                    IDutenteultcanc = table.Column<int>(type: "int", nullable: true),
                    dataoraultcanc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corsi", x => x.TipoCorsoID);
                });

            migrationBuilder.CreateTable(
                name: "DocenteTutor",
                columns: table => new
                {
                    DocenteTutorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cognome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tipologia = table.Column<int>(type: "int", nullable: true),
                    Qualifica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CF = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cellulare = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dataorains = table.Column<DateTime>(type: "datetime", nullable: false),
                    IDutenteins = table.Column<int>(type: "int", nullable: true),
                    dataoraultmod = table.Column<DateTime>(type: "datetime", nullable: true),
                    IDutenteultmod = table.Column<int>(type: "int", nullable: true),
                    eliminato = table.Column<short>(type: "smallint", nullable: false),
                    IDutenteultcanc = table.Column<int>(type: "int", nullable: true),
                    dataoraultcanc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocenteTutor", x => x.DocenteTutorID);
                });

            migrationBuilder.CreateTable(
                name: "Progetti",
                columns: table => new
                {
                    ProgettoFormativoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoCorsoID = table.Column<int>(type: "int", nullable: false),
                    titolo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    direttore_scientifico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipologia_evento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tutor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tutor_aula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ore_edizione = table.Column<int>(type: "int", nullable: true),
                    numero_partecipanti = table.Column<int>(type: "int", nullable: true),
                    sede_svolgimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataorains = table.Column<DateTime>(type: "datetime", nullable: true),
                    IDutenteins = table.Column<int>(type: "int", nullable: true),
                    dataoraultmod = table.Column<DateTime>(type: "datetime", nullable: true),
                    IDutenteultmod = table.Column<int>(type: "int", nullable: true),
                    eliminato = table.Column<short>(type: "smallint", nullable: true),
                    IDutenteultcanc = table.Column<int>(type: "int", nullable: true),
                    dataoraultcanc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progetti", x => x.ProgettoFormativoID);
                    table.ForeignKey(
                        name: "FK_Progetti_Corsi_TipoCorsoID",
                        column: x => x.TipoCorsoID,
                        principalTable: "Corsi",
                        principalColumn: "TipoCorsoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Edizioni",
                columns: table => new
                {
                    EdizioneCorsoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgettoFormativoID = table.Column<int>(type: "int", nullable: true),
                    Anno = table.Column<int>(type: "int", nullable: true),
                    NumeroEdizione = table.Column<int>(type: "int", nullable: true),
                    QuantitàModuli = table.Column<int>(type: "int", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataorains = table.Column<DateTime>(type: "datetime", nullable: true),
                    IDutenteins = table.Column<int>(type: "int", nullable: true),
                    dataoraultmod = table.Column<DateTime>(type: "datetime", nullable: true),
                    IDutenteultmod = table.Column<int>(type: "int", nullable: true),
                    eliminato = table.Column<short>(type: "smallint", nullable: true),
                    IDutenteultcanc = table.Column<int>(type: "int", nullable: true),
                    dataoraultcanc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edizioni", x => x.EdizioneCorsoID);
                    table.ForeignKey(
                        name: "FK_Edizioni_Progetti_ProgettoFormativoID",
                        column: x => x.ProgettoFormativoID,
                        principalTable: "Progetti",
                        principalColumn: "ProgettoFormativoID");
                });

            migrationBuilder.CreateTable(
                name: "Moduli",
                columns: table => new
                {
                    ModuloID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EdizioneCorsoID = table.Column<int>(type: "int", nullable: false),
                    data_inizio = table.Column<DateTime>(type: "datetime", nullable: false),
                    totale_ore = table.Column<int>(type: "int", nullable: false),
                    dataorains = table.Column<DateTime>(type: "datetime", nullable: true),
                    IDutenteins = table.Column<int>(type: "int", nullable: true),
                    dataoraultmod = table.Column<DateTime>(type: "datetime", nullable: true),
                    IDutenteultmod = table.Column<int>(type: "int", nullable: true),
                    eliminato = table.Column<short>(type: "smallint", nullable: true),
                    IDutenteultcanc = table.Column<int>(type: "int", nullable: true),
                    dataoraultcanc = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduli", x => x.ModuloID);
                    table.ForeignKey(
                        name: "FK_Moduli_Edizioni_EdizioneCorsoID",
                        column: x => x.EdizioneCorsoID,
                        principalTable: "Edizioni",
                        principalColumn: "EdizioneCorsoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edizioni_ProgettoFormativoID",
                table: "Edizioni",
                column: "ProgettoFormativoID");

            migrationBuilder.CreateIndex(
                name: "IX_Moduli_EdizioneCorsoID",
                table: "Moduli",
                column: "EdizioneCorsoID");

            migrationBuilder.CreateIndex(
                name: "IX_Progetti_TipoCorsoID",
                table: "Progetti",
                column: "TipoCorsoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocenteTutor");

            migrationBuilder.DropTable(
                name: "Moduli");

            migrationBuilder.DropTable(
                name: "Edizioni");

            migrationBuilder.DropTable(
                name: "Progetti");

            migrationBuilder.DropTable(
                name: "Corsi");
        }
    }
}
