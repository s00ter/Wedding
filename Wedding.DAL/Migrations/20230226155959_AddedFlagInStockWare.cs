﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.DAL.Migrations
{
    public partial class AddedFlagInStockWare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Wares",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Wares");
        }
    }
}
