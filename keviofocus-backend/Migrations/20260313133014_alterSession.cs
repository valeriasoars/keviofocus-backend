using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace keviofocus_backend.Migrations
{
    /// <inheritdoc />
    public partial class alterSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedCycles",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedCycles",
                table: "Sessions");
        }
    }
}
