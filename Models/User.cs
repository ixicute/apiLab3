using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkolprojektLab3.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [StringLength(14)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<UserInterestRT> Interests { get; set; }
    }
}
