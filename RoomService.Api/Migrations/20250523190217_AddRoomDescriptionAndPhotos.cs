using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomService.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomDescriptionAndPhotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Помещения",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoomPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomPhotos_Помещения_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Помещения",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomPhotos_RoomId",
                table: "RoomPhotos",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomPhotos");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Помещения");
        }
    }
}
