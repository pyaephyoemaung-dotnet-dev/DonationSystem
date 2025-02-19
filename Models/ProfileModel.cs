using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationSystem.Models
{
    [Table("tbl_profile")]
    public class ProfileModel
    {
        [Key]
        public int Id { get; set; }
        public string userId { get; set; }
        public string profile { get; set; }
        public DateTime created_at { get; set; } = System.DateTime.Now;
    }
}
