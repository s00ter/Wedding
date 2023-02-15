﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.DAL.Migrations
{
    public partial class AddedCategoryImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FileBytes",
                table: "WareCategories",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileBytes",
                table: "WareCategories");
        }
    }
}
