namespace SkolprojektLab3.Models.DTO
{
    public class GetAllUserDataDto
    {
        public string UserName { get; set; }

        public List<InterestsLinksDto> InterestsLinks { get; set; }
    }
}
