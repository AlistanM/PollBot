using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollBot.Data.Migrations
{
    public partial class AddChatTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatTitle",
                table: "ChatPolls",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatTitle",
                table: "ChatPolls");
        }
    }
}
