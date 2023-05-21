namespace SkolprojektLab3.Models.DTO
{
    public class InterestsLinksDto
    {
        public string Interest { get; set; }
        public string InterestDescription { get; set; }
        public List<UserLinksDto>? Links { get; set; }
    }
}
