using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkolprojektLab3.Data;
using SkolprojektLab3.Models;
using SkolprojektLab3.Models.DTO;
using SkolprojektLab3.Repos.IRepos;

namespace SkolprojektLab3.Repos
{
    public class GetInfoRepository : IGetInfoRepository
    {
        private readonly ApplicationDbContext context;

        public GetInfoRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<List<UserDto>> GetAllUsersAsync(string? filterUser)
        {
            var users = await (from u in context.Users
                               where u.FirstName.Contains(filterUser)
                               select new
                               {
                                   UserId = u.UserId,
                                   UserName = u.FirstName + " " + u.LastName,
                                   PhoneNumber = u.PhoneNumber

                               }).ToListAsync();

            List <UserDto> mappedUser = new List<UserDto>();

            foreach(var item in users)
            {
                var user = new UserDto();
                user.UserId = item.UserId;
                user.FullName = item.UserName;
                user.PhoneNumber = item.PhoneNumber;

                mappedUser.Add(user);
            }

            return mappedUser;
        }

        public async Task<List<UserInterestsDto>> GetInterestsByUserIdAsync(int userId)
        {
            var interests = await (from rt in context.UserInterestsRelationshipTable
                                   join u in context.Users on rt.FK_UserId equals u.UserId
                                   join i in context.Interests on rt.FK_InterestId equals i.InterestId
                                   where u.UserId == userId
                                   select new
                                   {
                                       Id = i.InterestId,
                                       InterestTitle = i.Title,
                                       Description = i.Description
                                   }).ToListAsync();

            List<UserInterestsDto> interestList = new List<UserInterestsDto>();

            foreach (var item in interests)
            {
                var interest = new UserInterestsDto();
                interest.interestId = item.Id;
                interest.Interest = item.InterestTitle;
                interest.InterestDescription = item.Description;

                interestList.Add(interest);
            }

            return interestList;
        }

        public async Task<List<UserLinksDto>> GetUserLinksAsync(int userId)
        {
            var links = await (from linkRT in context.LinksRelationshipTable
                               join uiRT in context.UserInterestsRelationshipTable on linkRT.FK_InterestUserId equals uiRT.Id
                               join u in context.Users on uiRT.FK_UserId equals u.UserId
                               join i in context.Interests on uiRT.FK_InterestId equals i.InterestId
                               where u.UserId == userId
                               select new
                               {
                                   Link = linkRT.Link
                               }).ToListAsync();

            List<UserLinksDto> userLinksList = new List<UserLinksDto>();

            foreach(var item in links)
            {
                var link = new UserLinksDto();

                link.Link = item.Link;

                userLinksList.Add(link);
            }

            if(userLinksList.Count == 0)
            {
                var link = new UserLinksDto();
                link.Link = "No links added yet.";
                userLinksList.Add(link);
            }

            return userLinksList;
        }

        public async Task<List<GetAllUserDataDto>> GetAllUsersDataAsync(int userId)
        {
            var users = await (from u in context.Users
                              where u.UserId == userId
                              select new
                              {
                                  UserName = u.FirstName + " " + u.LastName,
                                  PhoneNumber = u.PhoneNumber
                              }).ToListAsync();

            var userInterests = await (from uiRT in context.UserInterestsRelationshipTable
                                       join u in context.Users on uiRT.FK_UserId equals u.UserId
                                       join i in context.Interests on uiRT.FK_InterestId equals i.InterestId
                                       where u.UserId == userId
                                       select new
                                       {
                                           id = uiRT.Id,
                                           Interest = i.Title,
                                           Description = i.Description
                                       }).ToListAsync();

            var userLinks = await (from linkRT in context.LinksRelationshipTable
                                  join uiRT in context.UserInterestsRelationshipTable on linkRT.FK_InterestUserId equals uiRT.Id
                                  join u in context.Users on uiRT.FK_UserId equals u.UserId
                                  join i in context.Interests on uiRT.FK_InterestId equals i.InterestId
                                  where u.UserId == userId
                                  select new
                                  {
                                      InterestFK = linkRT.FK_InterestUserId,
                                      Link = linkRT.Link
                                  }).ToListAsync();

            List<GetAllUserDataDto> userData = new List<GetAllUserDataDto>();

            foreach (var u in users)
            {
                var user = new GetAllUserDataDto();
                user.UserName = u.UserName;
                user.InterestsLinks = new List<InterestsLinksDto>();

                foreach (var ui in userInterests)
                {
                    var interestsLinks = new InterestsLinksDto();
                    interestsLinks.Interest = ui.Interest;
                    interestsLinks.InterestDescription = ui.Description;
                    interestsLinks.Links = new List<UserLinksDto>();

                    foreach (var uL in userLinks)
                    {
                        if(uL.InterestFK == ui.id)
                        {
                            var link = new UserLinksDto();
                            link.Link = uL.Link;
                            interestsLinks.Links.Add(link);
                        }
                    }

                    user.InterestsLinks.Add(interestsLinks);
                }

                //if user has no interests added
                if (user.InterestsLinks.Count == 0)
                {
                    var interests = new InterestsLinksDto();
                    interests.Interest = "User hasn't added interests.";
                    interests.InterestDescription = "Empty";
                    interests.Links = new List<UserLinksDto>
                    {
                        new UserLinksDto()
                        {
                            Link = "No links added yet."
                        }
                    };

                    user.InterestsLinks.Add(interests);
                }
                userData.Add(user);
            }

            return userData;
        }

        public async Task<int> UserCountAsync()
        {
            int userCount = context.Users.Count();

            return userCount;
        }

        public async Task<int> InterestCountAsync()
        {
            int totalInterestCount = context.Interests.Count();

            return totalInterestCount;
        }

        public async Task<bool> CheckUserExistsAsync(int userId)
        {
            var user = await context.Users.FindAsync(userId);

            if(user != null)
            {
                return true;
            }

            return false;
        }
    }
}
