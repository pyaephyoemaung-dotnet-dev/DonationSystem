using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationSystem.Models
{
    [Table("tbl_web")]
    public class Webprofile
    {
        [Key]
        public int Id { get; set; }
        public string profile { get; set; }
        public string image { get; set; }
    }
}
