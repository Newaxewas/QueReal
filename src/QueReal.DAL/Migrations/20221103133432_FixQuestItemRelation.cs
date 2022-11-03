using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueReal.DAL.Migrations
{
    public partial class FixQuestItemRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestItem_Quest_QuestId",
                table: "QuestItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestId",
                table: "QuestItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestItem_Quest_QuestId",
                table: "QuestItem",
                column: "QuestId",
                principalTable: "Quest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestItem_Quest_QuestId",
                table: "QuestItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestId",
                table: "QuestItem",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestItem_Quest_QuestId",
                table: "QuestItem",
                column: "QuestId",
                principalTable: "Quest",
                principalColumn: "Id");
        }
    }
}
