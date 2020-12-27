using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesSubscriptions.DataAccess.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Genres = table.Column<string>(maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 100, nullable: false),
                    Premired = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    SessionTimeOut = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    ViewSubscriptions = table.Column<bool>(nullable: false),
                    CreateSubscriptions = table.Column<bool>(nullable: false),
                    UpdateSubscriptions = table.Column<bool>(nullable: false),
                    DeleteSubscriptions = table.Column<bool>(nullable: false),
                    ViewMovies = table.Column<bool>(nullable: false),
                    CreateMovies = table.Column<bool>(nullable: false),
                    UpdateMovies = table.Column<bool>(nullable: false),
                    DeleteMovies = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    WatchedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscription_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "City", "Email", "Name" },
                values: new object[,]
                {
                    { 1, "Nahariya", "lihi@yahoo.com", "Lihi Shitrit" },
                    { 2, "Nahariya", "gili@yahoo.com", "Gili Shitrit" },
                    { 3, "Nahariya", "omri@yahoo.com", "Omri Shitrit" },
                    { 4, "Nahariya", "shir@yahoo.com", "Shir Shitrit" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Genres", "ImageUrl", "Name", "Premired" },
                values: new object[,]
                {
                    { 1, "Drama, Romance", "http://static.tvmaze.com/uploads/images/original_untouched/203/509941.jpg", "The Affair", new DateTime(2014, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Drama, Thriller", "http://static.tvmaze.com/uploads/images/original_untouched/124/310268.jpg", "Scandal", new DateTime(2014, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserLogins",
                columns: new[] { "Id", "IsAdmin", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, true, "1234", "admin@yahoo.com" },
                    { 2, false, "1234", "nonAdmin@yahoo.com" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateMovies", "CreateSubscriptions", "CreatedDate", "DeleteMovies", "DeleteSubscriptions", "FirstName", "LastName", "SessionTimeOut", "UpdateMovies", "UpdateSubscriptions", "UserName", "ViewMovies", "ViewSubscriptions" },
                values: new object[,]
                {
                    { 1, true, true, new DateTime(2020, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Dani", "Shitrit", 9999, true, true, "admin@yahoo.com", true, true },
                    { 2, true, true, new DateTime(2020, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Yael", "Shitrit", 2, true, true, "nonAdmin@yahoo.com", true, true }
                });

            migrationBuilder.InsertData(
                table: "Subscription",
                columns: new[] { "Id", "MemberId", "MovieId", "WatchedDate" },
                values: new object[] { 1, 1, 1, new DateTime(2014, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Subscription",
                columns: new[] { "Id", "MemberId", "MovieId", "WatchedDate" },
                values: new object[] { 2, 1, 2, new DateTime(2014, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_MemberId",
                table: "Subscription",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_MovieId",
                table: "Subscription",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
