namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int CommentID { get; set; }

        public int? PostID { get; set; }

        public int UserID { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }
    }
}
