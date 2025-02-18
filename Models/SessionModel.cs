using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DonationSystem.Models
{
    [Table("tbl_session")]
    public class SessionModel
    {
        [Key]
        public int Id { get; set; }
        public string userId { get; set; }
        public string sessionId { get; set; }
        public DateTime sessionExpired { get; set; }   
    }
}
