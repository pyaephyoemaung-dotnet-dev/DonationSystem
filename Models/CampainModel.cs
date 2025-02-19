using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DonationSystem.Models
{
    [Table("tbl_campain")]
    public class CampainModel
    {
        [Key]
        public int Id { get; set; }
        public string? campainId { get; set; }
        public string? description { get; set; }
        public decimal? goal_amout { get; set; }
        public decimal? raised_amount { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public string? status { get; set; }
        public DateTime? created_at { get; set; }
    }
}
