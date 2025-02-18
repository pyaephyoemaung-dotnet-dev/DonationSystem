using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DonationSystem.Models
{
        [Table("tbl_signup")]
        public class SignupModel
        {
            [Key]
            public int Id { get; set; }
            public string userId { get; set; } = Guid.NewGuid().ToString();
        public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string role { get; set; }
            public string type { get; set; }
            public string address { get; set; }
            public string password { get; set; }
            public DateTime created_at { get; set; } = System.DateTime.Now;
    }
}
