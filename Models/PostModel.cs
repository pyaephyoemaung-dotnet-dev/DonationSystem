using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DonationSystem.Models
{
    [Table("tbl_post")]
    public class PostModel
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string postImage { get; set; }
        public string profile { get; set; }
        public string userId { get; set; }
        public DateTime? created_at { get; set; } = System.DateTime.Now;
        public string type { get; set; }
    }
}
