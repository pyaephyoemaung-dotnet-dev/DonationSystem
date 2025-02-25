using System.ComponentModel.DataAnnotations;

namespace DonationSystem.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DonorName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime DonationDate { get; set; } = DateTime.Now;
        public string userId { get; set; } 
        public string address { get; set; }
        public string phone { get; set; }
    }
}
