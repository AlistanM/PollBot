using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollBot.Data.Migrations
{
    public partial class AddPollInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PollName",
                table: "ChatPolls",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PollOptions",
                table: "ChatPolls",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PollName",
                table: "ChatPolls");

            migrationBuilder.DropColumn(
                name: "PollOptions",
                table: "ChatPolls");
        }
    }
}
