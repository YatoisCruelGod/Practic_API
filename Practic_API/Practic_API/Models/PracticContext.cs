using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practic_API.Models;

public partial class PracticContext : DbContext
{
    public PracticContext()
    {
    }

    public PracticContext(DbContextOptions<PracticContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Follower> Followers { get; set; }

    public virtual DbSet<HealthTip> HealthTips { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Postcategory> Postcategories { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Recipeingredient> Recipeingredients { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Commentid).HasName("PK__comments__CDA84BC583B389CB");

            entity.ToTable("comments");

            entity.HasIndex(e => e.Observerprofileid, "IX_comments_observerprofileid");

            entity.HasIndex(e => e.Postid, "IX_comments_postid");

            entity.Property(e => e.Commentid).HasColumnName("commentid");
            entity.Property(e => e.CommentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("comment_date");
            entity.Property(e => e.Observerprofileid).HasColumnName("observerprofileid");
            entity.Property(e => e.Postid).HasColumnName("postid");
            entity.Property(e => e.Textcomment)
                .HasColumnType("text")
                .HasColumnName("textcomment");

            entity.HasOne(d => d.Observerprofile).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Observerprofileid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__observ__4222D4EF");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Postid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comments__postid__412EB0B6");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Favoriteid).HasName("PK__favorite__87770BCD8DBF66F3");

            entity.ToTable("favorites");

            entity.HasIndex(e => e.Postid, "IX_favorites_postid");

            entity.HasIndex(e => e.Profileid, "IX_favorites_profileid");

            entity.Property(e => e.Favoriteid).HasColumnName("favoriteid");
            entity.Property(e => e.FavoriteDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("favorite_date");
            entity.Property(e => e.Postid).HasColumnName("postid");
            entity.Property(e => e.Profileid).HasColumnName("profileid");

            entity.HasOne(d => d.Post).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.Postid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__posti__4F7CD00D");

            entity.HasOne(d => d.Profile).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.Profileid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__favorites__profi__5070F446");
        });

        modelBuilder.Entity<Follower>(entity =>
        {
            entity.HasKey(e => new { e.Followerid, e.Followedid }).HasName("PK__follower__0C86679D45438D8A");

            entity.ToTable("followers");

            entity.HasIndex(e => e.Followedid, "IX_followers_followedid");

            entity.Property(e => e.Followerid).HasColumnName("followerid");
            entity.Property(e => e.Followedid).HasColumnName("followedid");
            entity.Property(e => e.FollowDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("follow_date");

            entity.HasOne(d => d.Followed).WithMany(p => p.FollowerFolloweds)
                .HasForeignKey(d => d.Followedid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__followers__follo__4BAC3F29");

            entity.HasOne(d => d.FollowerNavigation).WithMany(p => p.FollowerFollowerNavigations)
                .HasForeignKey(d => d.Followerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__followers__follo__4AB81AF0");
        });

        modelBuilder.Entity<HealthTip>(entity =>
        {
            entity.HasKey(e => e.Tipid).HasName("PK__health_t__E42200938FA2F0F3");

            entity.ToTable("health_tips");

            entity.HasIndex(e => e.Creatorprofileid, "IX_health_tips_creatorprofileid");

            entity.Property(e => e.Tipid).HasColumnName("tipid");
            entity.Property(e => e.Creatorprofileid).HasColumnName("creatorprofileid");
            entity.Property(e => e.TipDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("tip_date");
            entity.Property(e => e.TipText)
                .HasColumnType("text")
                .HasColumnName("tip_text");
            entity.Property(e => e.TipTitle)
                .HasMaxLength(255)
                .HasColumnName("tip_title");

            entity.HasOne(d => d.Creatorprofile).WithMany(p => p.HealthTips)
                .HasForeignKey(d => d.Creatorprofileid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__health_ti__creat__5DCAEF64");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.Likeid).HasName("PK__likes__4FC48EC36E05128F");

            entity.ToTable("likes");

            entity.HasIndex(e => e.Postid, "IX_likes_postid");

            entity.HasIndex(e => e.Profileid, "IX_likes_profileid");

            entity.Property(e => e.Likeid).HasColumnName("likeid");
            entity.Property(e => e.LikeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("like_date");
            entity.Property(e => e.Postid).HasColumnName("postid");
            entity.Property(e => e.Profileid).HasColumnName("profileid");

            entity.HasOne(d => d.Post).WithMany(p => p.Likes)
                .HasForeignKey(d => d.Postid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__likes__postid__45F365D3");

            entity.HasOne(d => d.Profile).WithMany(p => p.Likes)
                .HasForeignKey(d => d.Profileid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__likes__profileid__46E78A0C");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Postid).HasName("PK__post__DD017FD2C8B11C69");

            entity.ToTable("post");

            entity.HasIndex(e => e.Categoryid, "IX_post_categoryid");

            entity.HasIndex(e => e.Creatorprofileid, "IX_post_creatorprofileid");

            entity.Property(e => e.Postid).HasColumnName("postid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Creatorprofileid).HasColumnName("creatorprofileid");
            entity.Property(e => e.PostDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("post_date");
            entity.Property(e => e.PostText)
                .HasColumnType("text")
                .HasColumnName("post_text");
            entity.Property(e => e.PostTitle)
                .HasMaxLength(255)
                .HasColumnName("post_title");

            entity.HasOne(d => d.Category).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__post__categoryid__3D5E1FD2");

            entity.HasOne(d => d.Creatorprofile).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Creatorprofileid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__post__creatorpro__3C69FB99");

            entity.HasMany(d => d.Tags).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("Tagid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__post_tags__tagid__59FA5E80"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("Postid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__post_tags__posti__59063A47"),
                    j =>
                    {
                        j.HasKey("Postid", "Tagid").HasName("PK__post_tag__380ECF8EF29B6C23");
                        j.ToTable("post_tags");
                        j.HasIndex(new[] { "Tagid" }, "IX_post_tags_tagid");
                        j.IndexerProperty<int>("Postid").HasColumnName("postid");
                        j.IndexerProperty<int>("Tagid").HasColumnName("tagid");
                    });
        });

        modelBuilder.Entity<Postcategory>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("PK__postcate__23CDE590C90D33A9");

            entity.ToTable("postcategory");

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Profileid).HasName("PK__profile__7D466051178114DE");

            entity.ToTable("profile");

            entity.Property(e => e.Profileid).HasColumnName("profileid");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Recipeingredient>(entity =>
        {
            entity.HasKey(e => new { e.Recipeid, e.IngredientName }).HasName("PK__recipein__D8CE957E87E10019");

            entity.ToTable("recipeingredients");

            entity.Property(e => e.Recipeid).HasColumnName("recipeid");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(255)
                .HasColumnName("ingredient_name");
            entity.Property(e => e.Amount)
                .HasMaxLength(255)
                .HasColumnName("amount");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Recipeingredients)
                .HasForeignKey(d => d.Recipeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recipeing__recip__534D60F1");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Tagid).HasName("PK__tags__50FB05CFB6071D58");

            entity.ToTable("tags");

            entity.HasIndex(e => e.TagName, "UQ__tags__E298655CA58721E7").IsUnique();

            entity.Property(e => e.Tagid).HasColumnName("tagid");
            entity.Property(e => e.TagName)
                .HasMaxLength(255)
                .HasColumnName("tag_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
