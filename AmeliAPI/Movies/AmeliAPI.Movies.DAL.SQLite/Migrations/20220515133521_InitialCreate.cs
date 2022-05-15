using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmeliAPI.Movies.DAL.SQLite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Movies");

            migrationBuilder.CreateTable(
                name: "AssociatedPerson",
                schema: "Movies",
                columns: table => new
                {
                    AssociatedPersonID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Prename = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociatedPerson", x => x.AssociatedPersonID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                schema: "Movies",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                schema: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MovieTitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MovieSubtitle = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieID);
                });

            migrationBuilder.CreateTable(
                name: "MovieAssociatedPerson",
                schema: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "INTEGER", nullable: false),
                    AssociatedPersonID = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleFlags = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieAssociatedPerson", x => new { x.MovieID, x.AssociatedPersonID });
                    table.ForeignKey(
                        name: "FK_MovieAssociatedPerson_AssociatedPerson_AssociatedPersonID",
                        column: x => x.AssociatedPersonID,
                        principalSchema: "Movies",
                        principalTable: "AssociatedPerson",
                        principalColumn: "AssociatedPersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieAssociatedPerson_Movie_MovieID",
                        column: x => x.MovieID,
                        principalSchema: "Movies",
                        principalTable: "Movie",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenre",
                schema: "Movies",
                columns: table => new
                {
                    MovieID = table.Column<int>(type: "INTEGER", nullable: false),
                    GenreID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenre", x => new { x.MovieID, x.GenreID });
                    table.ForeignKey(
                        name: "FK_MovieGenre_Genre_GenreID",
                        column: x => x.GenreID,
                        principalSchema: "Movies",
                        principalTable: "Genre",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovieGenre_Movie_MovieID",
                        column: x => x.MovieID,
                        principalSchema: "Movies",
                        principalTable: "Movie",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieAssociatedPerson_AssociatedPersonID",
                schema: "Movies",
                table: "MovieAssociatedPerson",
                column: "AssociatedPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_GenreID",
                schema: "Movies",
                table: "MovieGenre",
                column: "GenreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieAssociatedPerson",
                schema: "Movies");

            migrationBuilder.DropTable(
                name: "MovieGenre",
                schema: "Movies");

            migrationBuilder.DropTable(
                name: "AssociatedPerson",
                schema: "Movies");

            migrationBuilder.DropTable(
                name: "Genre",
                schema: "Movies");

            migrationBuilder.DropTable(
                name: "Movie",
                schema: "Movies");
        }
    }
}
