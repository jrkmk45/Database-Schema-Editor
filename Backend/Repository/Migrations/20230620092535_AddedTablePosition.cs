using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedTablePosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_DataType_DataTypeId",
                table: "Attributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataType",
                table: "DataType");

            migrationBuilder.RenameTable(
                name: "DataType",
                newName: "DataTypes");

            migrationBuilder.AddColumn<int>(
                name: "X",
                table: "Tables",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Y",
                table: "Tables",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataTypes",
                table: "DataTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_DataTypes_DataTypeId",
                table: "Attributes",
                column: "DataTypeId",
                principalTable: "DataTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_DataTypes_DataTypeId",
                table: "Attributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataTypes",
                table: "DataTypes");

            migrationBuilder.DropColumn(
                name: "X",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Tables");

            migrationBuilder.RenameTable(
                name: "DataTypes",
                newName: "DataType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataType",
                table: "DataType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_DataType_DataTypeId",
                table: "Attributes",
                column: "DataTypeId",
                principalTable: "DataType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
