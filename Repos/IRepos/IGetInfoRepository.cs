using Microsoft.AspNetCore.Mvc;
using SkolprojektLab3.Models.DTO;

namespace SkolprojektLab3.Repos.IRepos
{
    public interface IGetInfoRepository
    {
        Task<List<UserDto>> GetAllUsersAsync(string? filterUser);

        Task<List<UserInterestsDto>> GetInterestsByUserIdAsync(int userId);

        Task<List<UserLinksDto>> GetUserLinksAsync(int userId);

        Task<List<GetAllUserDataDto>> GetAllUsersDataAsync(int userId);

        Task<int> UserCountAsync();

        Task<int> InterestCountAsync();

        Task<bool> CheckUserExistsAsync(int userId);

    }
}
