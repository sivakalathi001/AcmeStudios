using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeStudios.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudioItemTypes",
                columns: table => new
                {
                    StudioItemTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioItemTypes", x => x.StudioItemTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StudioItems",
                columns: table => new
                {
                    StudioItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Acquired = table.Column<DateTime>(nullable: false),
                    Sold = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    SoldFor = table.Column<decimal>(nullable: true),
                    Eurorack = table.Column<bool>(nullable: false),
                    StudioItemTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioItems", x => x.StudioItemId);
                    table.ForeignKey(
                        name: "FK_StudioItems_StudioItemTypes_StudioItemTypeId",
                        column: x => x.StudioItemTypeId,
                        principalTable: "StudioItemTypes",
                        principalColumn: "StudioItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StudioItemTypes",
                columns: new[] { "StudioItemTypeId", "Value" },
                values: new object[,]
                {
                    { 1, "Synthesiser" },
                    { 2, "Drum Machine" },
                    { 3, "Effect" },
                    { 4, "Sequencer" },
                    { 5, "Mixer" },
                    { 6, "Oscillator" },
                    { 7, "Utility" }
                });

            
            migrationBuilder.CreateIndex(
                name: "IX_StudioItems_StudioItemTypeId",
                table: "StudioItems",
                column: "StudioItemTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
                name: "StudioItems");

            migrationBuilder.DropTable(
                name: "StudioItemTypes");
        }
    }
}
