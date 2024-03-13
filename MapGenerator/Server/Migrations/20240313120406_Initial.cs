using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TileSets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TileSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TileSets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneratedMaps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    N = table.Column<int>(type: "int", nullable: false),
                    M = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TileSetId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedMaps_TileSets_TileSetId",
                        column: x => x.TileSetId,
                        principalTable: "TileSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    P3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symmetry = table.Column<int>(type: "int", nullable: false),
                    TileSetId = table.Column<long>(type: "bigint", nullable: false),
                    Stream = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tiles_TileSets_TileSetId",
                        column: x => x.TileSetId,
                        principalTable: "TileSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetTiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    N = table.Column<int>(type: "int", nullable: false),
                    M = table.Column<int>(type: "int", nullable: false),
                    GeneratedMapId = table.Column<long>(type: "bigint", nullable: false),
                    TileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetTiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetTiles_GeneratedMaps_GeneratedMapId",
                        column: x => x.GeneratedMapId,
                        principalTable: "GeneratedMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetTiles_Tiles_TileId",
                        column: x => x.TileId,
                        principalTable: "Tiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GeneratedMapId = table.Column<long>(type: "bigint", nullable: false),
                    TileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weights_GeneratedMaps_GeneratedMapId",
                        column: x => x.GeneratedMapId,
                        principalTable: "GeneratedMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Weights_Tiles_TileId",
                        column: x => x.TileId,
                        principalTable: "Tiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedMaps_TileSetId",
                table: "GeneratedMaps",
                column: "TileSetId");

            migrationBuilder.CreateIndex(
                name: "IX_SetTiles_GeneratedMapId",
                table: "SetTiles",
                column: "GeneratedMapId");

            migrationBuilder.CreateIndex(
                name: "IX_SetTiles_TileId",
                table: "SetTiles",
                column: "TileId");

            migrationBuilder.CreateIndex(
                name: "IX_Tiles_TileSetId",
                table: "Tiles",
                column: "TileSetId");

            migrationBuilder.CreateIndex(
                name: "IX_TileSets_UserId",
                table: "TileSets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weights_GeneratedMapId",
                table: "Weights",
                column: "GeneratedMapId");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_TileId",
                table: "Weights",
                column: "TileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SetTiles");

            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.DropTable(
                name: "GeneratedMaps");

            migrationBuilder.DropTable(
                name: "Tiles");

            migrationBuilder.DropTable(
                name: "TileSets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
