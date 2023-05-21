using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SkolprojektLab3.Models
{
    public class Interest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InterestId { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(150)]
        public string? Description { get; set; }

        public virtual ICollection<UserInterestRT> Users { get; set; }
    }
}
