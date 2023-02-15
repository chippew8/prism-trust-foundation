using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class donationRecipient
    {

        public string? NRIC { get; set; }

       
        public string? address { get; set; }

        [MaxLength(30)]
        public string? Fname { get; set; }

       public string? nationality { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

       public decimal monthlyIncome { get; set; }
        [Key]
        public string? email { get; set; }


    }
}
