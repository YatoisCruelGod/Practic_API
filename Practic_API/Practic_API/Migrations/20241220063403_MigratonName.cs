using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practic_API.Migrations
{
    /// <inheritdoc />
    public partial class MigratonName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "postcategory",
                columns: table => new
                {
                    categoryid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__postcate__23CDE590C90D33A9", x => x.categoryid);
                });

            migrationBuilder.CreateTable(
                name: "profile",
                columns: table => new
                {
                    profileid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__profile__7D466051178114DE", x => x.profileid);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    tagid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tags__50FB05CFB6071D58", x => x.tagid);
                });

            migrationBuilder.CreateTable(
                name: "followers",
                columns: table => new
                {
                    followerid = table.Column<int>(type: "int", nullable: false),
                    followedid = table.Column<int>(type: "int", nullable: false),
                    follow_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__follower__0C86679D45438D8A", x => new { x.followerid, x.followedid });
                    table.ForeignKey(
                        name: "FK__followers__follo__4AB81AF0",
                        column: x => x.followerid,
                        principalTable: "profile",
                        principalColumn: "profileid");
                    table.ForeignKey(
                        name: "FK__followers__follo__4BAC3F29",
                        column: x => x.followedid,
                        principalTable: "profile",
                        principalColumn: "profileid");
                });

            migrationBuilder.CreateTable(
                name: "health_tips",
                columns: table => new
                {
                    tipid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creatorprofileid = table.Column<int>(type: "int", nullable: false),
                    tip_title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    tip_text = table.Column<string>(type: "text", nullable: false),
                    tip_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__health_t__E42200938FA2F0F3", x => x.tipid);
                    table.ForeignKey(
                        name: "FK__health_ti__creat__5DCAEF64",
                        column: x => x.creatorprofileid,
                        principalTable: "profile",
                        principalColumn: "profileid");
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    postid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    creatorprofileid = table.Column<int>(type: "int", nullable: false),
                    categoryid = table.Column<int>(type: "int", nullable: false),
                    post_title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    post_text = table.Column<string>(type: "text", nullable: false),
                    post_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__post__DD017FD2C8B11C69", x => x.postid);
                    table.ForeignKey(
                        name: "FK__post__categoryid__3D5E1FD2",
                        column: x => x.categoryid,
                        principalTable: "postcategory",
                        principalColumn: "categoryid");
                    table.ForeignKey(
                        name: "FK__post__creatorpro__3C69FB99",
                        column: x => x.creatorprofileid,
                        principalTable: "profile",
                        principalColumn: "profileid");
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    commentid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postid = table.Column<int>(type: "int", nullable: false),
                    observerprofileid = table.Column<int>(type: "int", nullable: false),
                    textcomment = table.Column<string>(type: "text", nullable: false),
                    comment_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comments__CDA84BC583B389CB", x => x.commentid);
                    table.ForeignKey(
                        name: "FK__comments__observ__4222D4EF",
                        column: x => x.observerprofileid,
                        principalTable: "profile",
                        principalColumn: "profileid");
                    table.ForeignKey(
                        name: "FK__comments__postid__412EB0B6",
                        column: x => x.postid,
                        principalTable: "post",
                        principalColumn: "postid");
                });

            migrationBuilder.CreateTable(
                name: "favorites",
                columns: table => new
                {
                    favoriteid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postid = table.Column<int>(type: "int", nullable: false),
                    profileid = table.Column<int>(type: "int", nullable: false),
                    favorite_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__favorite__87770BCD8DBF66F3", x => x.favoriteid);
                    table.ForeignKey(
                        name: "FK__favorites__posti__4F7CD00D",
                        column: x => x.postid,
                        principalTable: "post",
                        principalColumn: "postid");
                    table.ForeignKey(
                        name: "FK__favorites__profi__5070F446",
                        column: x => x.profileid,
                        principalTable: "profile",
                        principalColumn: "profileid");
                });

            migrationBuilder.CreateTable(
                name: "likes",
                columns: table => new
                {
                    likeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    postid = table.Column<int>(type: "int", nullable: false),
                    profileid = table.Column<int>(type: "int", nullable: false),
                    like_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__likes__4FC48EC36E05128F", x => x.likeid);
                    table.ForeignKey(
                        name: "FK__likes__postid__45F365D3",
                        column: x => x.postid,
                        principalTable: "post",
                        principalColumn: "postid");
                    table.ForeignKey(
                        name: "FK__likes__profileid__46E78A0C",
                        column: x => x.profileid,
                        principalTable: "profile",
                        principalColumn: "profileid");
                });

            migrationBuilder.CreateTable(
                name: "post_tags",
                columns: table => new
                {
                    postid = table.Column<int>(type: "int", nullable: false),
                    tagid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__post_tag__380ECF8EF29B6C23", x => new { x.postid, x.tagid });
                    table.ForeignKey(
                        name: "FK__post_tags__posti__59063A47",
                        column: x => x.postid,
                        principalTable: "post",
                        principalColumn: "postid");
                    table.ForeignKey(
                        name: "FK__post_tags__tagid__59FA5E80",
                        column: x => x.tagid,
                        principalTable: "tags",
                        principalColumn: "tagid");
                });

            migrationBuilder.CreateTable(
                name: "recipeingredients",
                columns: table => new
                {
                    recipeid = table.Column<int>(type: "int", nullable: false),
                    ingredient_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    amount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__recipein__D8CE957E87E10019", x => new { x.recipeid, x.ingredient_name });
                    table.ForeignKey(
                        name: "FK__recipeing__recip__534D60F1",
                        column: x => x.recipeid,
                        principalTable: "post",
                        principalColumn: "postid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_comments_observerprofileid",
                table: "comments",
                column: "observerprofileid");

            migrationBuilder.CreateIndex(
                name: "IX_comments_postid",
                table: "comments",
                column: "postid");

            migrationBuilder.CreateIndex(
                name: "IX_favorites_postid",
                table: "favorites",
                column: "postid");

            migrationBuilder.CreateIndex(
                name: "IX_favorites_profileid",
                table: "favorites",
                column: "profileid");

            migrationBuilder.CreateIndex(
                name: "IX_followers_followedid",
                table: "followers",
                column: "followedid");

            migrationBuilder.CreateIndex(
                name: "IX_health_tips_creatorprofileid",
                table: "health_tips",
                column: "creatorprofileid");

            migrationBuilder.CreateIndex(
                name: "IX_likes_postid",
                table: "likes",
                column: "postid");

            migrationBuilder.CreateIndex(
                name: "IX_likes_profileid",
                table: "likes",
                column: "profileid");

            migrationBuilder.CreateIndex(
                name: "IX_post_categoryid",
                table: "post",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_post_creatorprofileid",
                table: "post",
                column: "creatorprofileid");

            migrationBuilder.CreateIndex(
                name: "IX_post_tags_tagid",
                table: "post_tags",
                column: "tagid");

            migrationBuilder.CreateIndex(
                name: "UQ__tags__E298655CA58721E7",
                table: "tags",
                column: "tag_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "favorites");

            migrationBuilder.DropTable(
                name: "followers");

            migrationBuilder.DropTable(
                name: "health_tips");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropTable(
                name: "post_tags");

            migrationBuilder.DropTable(
                name: "recipeingredients");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "postcategory");

            migrationBuilder.DropTable(
                name: "profile");
        }
    }
}
