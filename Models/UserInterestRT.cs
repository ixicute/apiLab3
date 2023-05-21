using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkolprojektLab3.Models
{
    public class UserInterestRT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int FK_UserId { get; set; }
        public User Users { get; set; }

        [Required]
        [ForeignKey("Interests")]
        public int FK_InterestId { get; set; }
        public Interest Interests { get; set; }

        public virtual ICollection<LinkRT> Links { get; set; }
    }
}
