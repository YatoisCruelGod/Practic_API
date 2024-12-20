using System;
using System.Collections.Generic;

namespace Practic_API.Models;

public partial class Follower
{
    public int Followerid { get; set; }

    public int Followedid { get; set; }

    public DateTime? FollowDate { get; set; }

    public virtual Profile Followed { get; set; } = null!;

    public virtual Profile FollowerNavigation { get; set; } = null!;
}
