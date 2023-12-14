using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NadinSoftTask.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCreatorOfProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "Products",
                newName: "OperatorId");

            migrationBuilder.AddColumn<string>(
                name: "OperatorName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperatorName",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "OperatorId",
                table: "Products",
                newName: "Creator");
        }
    }
}
