using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WiseSwitch.Migrations
{
    /// <inheritdoc />
    public partial class MakeSwitchModelFksTutorialAndScriptNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwitchModels_Scripts_ScriptId",
                table: "SwitchModels");

            migrationBuilder.DropForeignKey(
                name: "FK_SwitchModels_Tutorials_TutorialId",
                table: "SwitchModels");

            migrationBuilder.AlterColumn<int>(
                name: "TutorialId",
                table: "SwitchModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ScriptId",
                table: "SwitchModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SwitchModels_Scripts_ScriptId",
                table: "SwitchModels",
                column: "ScriptId",
                principalTable: "Scripts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SwitchModels_Tutorials_TutorialId",
                table: "SwitchModels",
                column: "TutorialId",
                principalTable: "Tutorials",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SwitchModels_Scripts_ScriptId",
                table: "SwitchModels");

            migrationBuilder.DropForeignKey(
                name: "FK_SwitchModels_Tutorials_TutorialId",
                table: "SwitchModels");

            migrationBuilder.AlterColumn<int>(
                name: "TutorialId",
                table: "SwitchModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScriptId",
                table: "SwitchModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SwitchModels_Scripts_ScriptId",
                table: "SwitchModels",
                column: "ScriptId",
                principalTable: "Scripts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SwitchModels_Tutorials_TutorialId",
                table: "SwitchModels",
                column: "TutorialId",
                principalTable: "Tutorials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
