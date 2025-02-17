using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationSystem.Models
{
    [Table("tbl_post")]
    public class PostModel
    {
        [Key]
        public int Id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? postImage { get; set; }
        public string? profile { get; set; }
        public string? userId { get; set; }
        public DateTime? created_at { get; set; }
    }
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
    [Table("tbl_list")]
    public class ListModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? categories { get; set; }
        public string? description { get; set; }
        public string? name { get; set; }
        public string? profile { get; set; }
    }
}
