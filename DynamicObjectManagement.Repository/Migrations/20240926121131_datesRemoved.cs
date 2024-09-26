using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicObjectManagement.Repository.Migrations
{
    /// <inheritdoc />
    public partial class datesRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "DynamicObjects");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "DynamicObjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "DynamicObjects",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "DynamicObjects",
                type: "datetime2",
                nullable: true);
        }
    }
}
