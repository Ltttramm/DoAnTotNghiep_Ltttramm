namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChatbotHistory")]
    public partial class ChatbotHistory
    {
        [Key]
        public int ChatID { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "text")]
        public string UserMessage { get; set; }

        [Column(TypeName = "text")]
        public string BotResponse { get; set; }

        public DateTime? Timestamp { get; set; }

        public virtual User User { get; set; }
    }
}
