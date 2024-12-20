using Practic_API.Models;
using System.Linq;

namespace Practic_API.Payloads
{
    public record PostResponse(
        int Id,
        string Title,
        string Text,
        DateTime? UploadDate,
        int CategoryId,
        int CreatorId,
        List<TagResponse> Tags,
        List<CommentResponse> Comments,
        int Likes,
        int Favorites
    )
    {
        public PostResponse(Post post) :
            this(
                post.Postid,
                post.PostTitle,
                post.PostText,
                post.PostDate,
                post.Categoryid,
                post.Creatorprofileid,
                post.Tags.Select(tag => new TagResponse(tag)).ToList(),
                post.Comments.Select(comment => new CommentResponse(comment)).ToList(),
                post.Likes.Select(like => new LikeResponse(like)).Count(),
                post.Favorites.Select(fav => new FavoriteResponse(fav)).Count()
            )
        { }
    }

    public record TagResponse(
        int Id,
        string Name
    )
    {
        public TagResponse(Tag tag) : this(
            tag.Tagid,
            tag.TagName
        )
        { }

    }

    public record CommentResponse(
        int CommentId,
        int PostId,
        int ObserverProfileId,
        string TextComment,
        DateTime? CommentDate
    )
    {
        public CommentResponse(Comment comment) : this(
            comment.Commentid,
            comment.Postid,
            comment.Observerprofileid,
            comment.Textcomment,
            comment.CommentDate
        )
        { }
    }

    public record LikeResponse(
        int LikeId,
        int PostId,
        int ProfileId,
        DateTime? LikeDate
    )
    {
        public LikeResponse(Like like) : this(
            like.Likeid,
            like.Postid,
            like.Profileid,
            like.LikeDate
        )
        { }
    }

    public record FavoriteResponse(
        int FavoriteId,
        int PostId,
        int ProfileId,
        DateTime? FavoriteDate
    )
    {
        public FavoriteResponse(Favorite favorite) : this(
            favorite.Favoriteid,
            favorite.Postid,
            favorite.Profileid,
            favorite.FavoriteDate
        )
        { }
    }
}
