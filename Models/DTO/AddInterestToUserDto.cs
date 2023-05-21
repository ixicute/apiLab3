using System.ComponentModel.DataAnnotations;

namespace SkolprojektLab3.Models.DTO
{
    //First add the new interest, then:
    /*
     * var lastInsertedInterest = await context.Interests.LastOrDefaultAsync();
     * int lastAddedId = lastInsertedUser?.Id ?? 0;
     * 
     * This way we get the id of that interest and can then use it to set the connection in the RT table.
     */
    public class AddInterestToUserDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string InterestTitle { get; set; }

        public string? InterestDescription { get; set; }
    }
}
