using System.ComponentModel.DataAnnotations;

namespace SkolprojektLab3.Models.DTO
{
    public class AddInterestLinkDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int InterestId { get; set; }
        [Required]
        public string Link { get; set; }
    }
}
