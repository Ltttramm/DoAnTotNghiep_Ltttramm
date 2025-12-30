namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Like
    {
        public int LikeID { get; set; }

        public int? PostID { get; set; }

        public int UserID { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}
