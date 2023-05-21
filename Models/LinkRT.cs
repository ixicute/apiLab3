using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkolprojektLab3.Models
{
    public class LinkRT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserInterest")]
        public int FK_InterestUserId { get; set; }
        public UserInterestRT UserInterest { get; set; }

        public string? Link { get; set; }
    }
}
