using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    Headliners = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryMovie",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMovie", x => new { x.CategoryId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_CategoryMovie_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CommentCount", "CreatedDate", "Description", "Headliners", "LikeCount", "ModifiedDate", "Thumbnail", "Title", "ViewCount", "Year" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2021, 10, 27, 13, 57, 53, 922, DateTimeKind.Local).AddTicks(6746), "<p>Shang-Chi ve On Halka Efsanesi, Marvel Comics karakteri Shang-Chi'yi temel alan yayınlanacak bir Amerikan süper kahraman filmi. Marvel Studios tarafından üretilen ve Walt Disney Pictures tarafından dağıtılan filmin, Marvel Sinematik Evreni'nin 25. filmi olması planlandı. Film Destin Daniel Cretton tarafından yönetildi, senaryosu David Callaham'a aitti ve başrolleri Simu Liu ve Tony Leung canlandırdı.</p>", null, 37, new DateTime(2021, 10, 27, 13, 57, 53, 922, DateTimeKind.Local).AddTicks(7117), "movieImages/defaultThumbnail.jpg", "Shang-Chi ve on Halka Efsanesi", 125, new DateTime(2021, 10, 27, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 7, new DateTime(2021, 10, 27, 13, 57, 53, 922, DateTimeKind.Local).AddTicks(8394), "<p>Shang-Chi ve On Halka Efsanesi, Marvel Comics karakteri Shang-Chi'yi temel alan yayınlanacak bir Amerikan süper kahraman filmi. Marvel Studios tarafından üretilen ve Walt Disney Pictures tarafından dağıtılan filmin, Marvel Sinematik Evreni'nin 25. filmi olması planlandı. Film Destin Daniel Cretton tarafından yönetildi, senaryosu David Callaham'a aitti ve başrolleri Simu Liu ve Tony Leung canlandırdı.</p>", null, 88, new DateTime(2021, 10, 27, 13, 57, 53, 922, DateTimeKind.Local).AddTicks(8395), "movieImages/defaultThumbnail.jpg", "Dune: Çöl Gezegeni", 195, new DateTime(2021, 10, 27, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, 14, new DateTime(2021, 10, 27, 13, 57, 53, 922, DateTimeKind.Local).AddTicks(8402), "<p>Shang-Chi ve On Halka Efsanesi, Marvel Comics karakteri Shang-Chi'yi temel alan yayınlanacak bir Amerikan süper kahraman filmi. Marvel Studios tarafından üretilen ve Walt Disney Pictures tarafından dağıtılan filmin, Marvel Sinematik Evreni'nin 25. filmi olması planlandı. Film Destin Daniel Cretton tarafından yönetildi, senaryosu David Callaham'a aitti ve başrolleri Simu Liu ve Tony Leung canlandırdı.</p>", null, 118, new DateTime(2021, 10, 27, 13, 57, 53, 922, DateTimeKind.Local).AddTicks(8404), "movieImages/defaultThumbnail.jpg", "Örümcek-Adam: Eve Dönüş Yok", 272, new DateTime(2021, 10, 27, 0, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 22, "516bac10-9caf-4f90-82ed-aa9f58ee7c8b", "SuperAdmin", "SUPERADMIN" },
                    { 21, "f0c093ec-fd9f-4f5c-828d-d7cc98abee5e", "AdminArea.Home.Read", "ADMINAREA.HOME.READ" },
                    { 20, "aa47ea85-bc35-45b4-b1fd-4f43becf8e98", "Comment.Delete", "COMMENT.DELETE" },
                    { 19, "5cb00880-fecb-45d7-9f44-1023385ba8f8", "Comment.Update", "COMMENT.UPDATE" },
                    { 18, "9fcdda7a-2457-400e-b09c-32388000810a", "Comment.Read", "COMMENT.READ" },
                    { 17, "e9410bb3-88b4-476c-b37b-7f96bf94cf0e", "Comment.Create", "COMMENT.CREATE" },
                    { 16, "7c9317fb-d0c3-49f0-ae90-093814ef5ac8", "Role.Delete", "ROLE.DELETE" },
                    { 15, "353c1992-4154-412f-b69d-a26d2cb92c04", "Role.Update", "ROLE.UPDATE" },
                    { 14, "7133df5e-f296-49df-ab88-54662226d0a0", "Role.Read", "ROLE.READ" },
                    { 13, "716d9493-26ba-4565-8a30-231619a8fbff", "Role.Create", "ROLE.CREATE" },
                    { 12, "4b308d70-5c98-49ef-8349-7d0f749d2199", "User.Delete", "USER.DELETE" },
                    { 11, "3fd80bf7-228f-4b6a-9279-73fbe00d935e", "User.Update", "USER.UPDATE" },
                    { 10, "426ac12f-ed80-409d-8889-3664202647e4", "User.Read", "USER.READ" },
                    { 9, "77e475f7-1f4a-4433-b7f1-6c814d62fe64", "User.Create", "USER.CREATE" },
                    { 8, "389d040e-2d41-4f15-891e-6dafa2ca732c", "Movie.Delete", "MOVIE.DELETE" },
                    { 7, "98326f79-3562-4832-aec2-7cc4cb0c11d8", "Movie.Update", "MOVIE.UPDATE" },
                    { 6, "a229d165-8950-44d4-b602-b6cdbe411dee", "Movie.Read", "MOVIE.READ" },
                    { 5, "40444ec9-896e-4547-9f78-e3bcedae5e34", "Movie.Create", "MOVIE.CREATE" },
                    { 4, "cbb06edd-58ac-44a9-b974-2ce68343c622", "Category.Delete", "CATEGORY.DELETE" },
                    { 3, "4b3d7615-3aba-4429-a96b-0783229a7cda", "Category.Update", "CATEGORY.UPDATE" },
                    { 2, "5cdbe379-b5f5-4fe2-81a2-d50560faa460", "Category.Read", "CATEGORY.READ" },
                    { 1, "76673bd4-2a36-4fe6-935a-4bd318b654c1", "Category.Create", "CATEGORY.CREATE" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "b654e5f0-caed-40b7-8f4d-5cff4c25d249", "adminuser@gmail.com", true, false, null, "ADMINUSER@GMAIL.COM", "ADMINUSER", "AQAAAAEAACcQAAAAEAgKuUSXQqNS82SQQVrd4pNMqt0pSAhlyNkKXd3A7tpZ7vvb1dBp1HKD6/B9Sg3D+Q==", "+905555555555", true, "/userImages/defaultUser.png", "2f0b8a94-1503-4ddb-9713-bcb0bb906fb6", false, "adminuser" },
                    { 2, 0, "16740ac9-8bbc-4029-bdbf-399f969d9617", "editorUser@gmail.com", true, false, null, "EDITORUSER@GMAIL.COM", "EDITORUSER", "AQAAAAEAACcQAAAAEMCH+fplNAfF/7FuherVsAfzVCzG2KUJegKwHu4uYR5N5pnbb3IcNEEaLXImFCCfzQ==", "+905555555555", true, "/userImages/defaultUser.png", "3ca33626-34ea-49e8-821b-abe1e794de91", false, "editorUser" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "MovieId", "UserId", "UserName" },
                values: new object[,]
                {
                    { 1, "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır.", 1, 1, "adminuser" },
                    { 2, "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır.", 2, 1, "adminuser" },
                    { 3, "Lorem Ipsum pasajlarının birçok çeşitlemesi vardır. Ancak bunların büyük bir çoğunluğu mizah katılarak veya rastgele sözcükler eklenerek değiştirilmişlerdir. Eğer bir Lorem Ipsum pasajı kullanacaksanız, metin aralarına utandırıcı sözcükler gizlenmediğinden emin olmanız gerekir. İnternet'teki tüm Lorem Ipsum üreteçleri önceden belirlenmiş metin bloklarını yineler. Bu da, bu üreteci İnternet üzerindeki gerçek Lorem Ipsum üreteci yapar. Bu üreteç, 200'den fazla Latince sözcük ve onlara ait cümle yapılarını içeren bir sözlük kullanır. Bu nedenle, üretilen Lorem Ipsum metinleri yinelemelerden, mizahtan ve karakteristik olmayan sözcüklerden uzaktır.", 3, 2, "editoruser" }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "MovieId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 20, 1 },
                    { 21, 1 },
                    { 22, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 5, 2 },
                    { 19, 1 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 17, 2 },
                    { 18, 2 },
                    { 19, 2 },
                    { 4, 2 },
                    { 18, 1 },
                    { 17, 1 },
                    { 16, 1 },
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 20, 2 },
                    { 21, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMovie_MovieId",
                table: "CategoryMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MovieId",
                table: "Comments",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_MovieId",
                table: "Likes",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMovie");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
