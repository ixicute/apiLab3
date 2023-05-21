using Microsoft.AspNetCore.Mvc;
using SkolprojektLab3.Models;
using SkolprojektLab3.Models.DTO;

namespace SkolprojektLab3.Repos.IRepos
{
    public interface IPostDataRepository
    {
        Task<bool> AddNewUserAsync(NewUserDto newUser);
        Task<bool> AddnConnectInterestToUserAsync(AddInterestToUserDto userInterest);
        Task<bool> AddLinkToInterestAsync(AddInterestLinkDto interestLink);

        Task<bool> RemoveUserAsync(int userId);
    }
}
