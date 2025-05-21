using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartEvent.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameMaxAttendeesToMaxParticipants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "maxAttendees",
                table: "Events",
                newName: "MaxParticipants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxParticipants",
                table: "Events",
                newName: "maxAttendees");
        }
    }
}
