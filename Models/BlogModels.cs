using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationSystem.Models
{
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
