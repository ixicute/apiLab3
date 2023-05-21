using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkolprojektLab3.Data;
using SkolprojektLab3.Models;
using SkolprojektLab3.Models.DTO;
using SkolprojektLab3.Repos.IRepos;

namespace SkolprojektLab3.Repos
{
    public class PostInfoRepository : IPostDataRepository
    {
        private readonly ApplicationDbContext context;

        public PostInfoRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> AddNewUserAsync(NewUserDto newUser)
        {

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var addUser = new User()
                {
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    PhoneNumber = newUser.PhoneNumber
                };
                await context.Users.AddAsync(addUser);

                await context.SaveChangesAsync();

                transaction.CommitAsync();

                return true;
            }

            catch(Exception)
            {
                await transaction.RollbackAsync();

                return false;
            }
        }

        public async Task<bool> AddnConnectInterestToUserAsync(AddInterestToUserDto userInterest)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var addInterest = new Interest()
                {
                    Title = userInterest.InterestTitle,
                    Description = userInterest.InterestDescription == "string" ? "" : userInterest.InterestDescription
                };

                await context.Interests.AddAsync(addInterest);

                await context.SaveChangesAsync();

                var lastInterest = await context.Interests.OrderBy(x => x.InterestId).LastOrDefaultAsync();

                var newConnectionUserToInterest = new UserInterestRT()
                {
                    FK_UserId = userInterest.UserId,
                    FK_InterestId = lastInterest.InterestId
                };

                await context.UserInterestsRelationshipTable.AddAsync(newConnectionUserToInterest);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }

            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> AddLinkToInterestAsync(AddInterestLinkDto interestLink)
        {
            int userInterestRowId = await (from rt in context.UserInterestsRelationshipTable
                                      join u in context.Users on rt.FK_UserId equals u.UserId
                                      join i in context.Interests on rt.FK_InterestId equals i.InterestId
                                      where u.UserId == interestLink.UserId && i.InterestId == interestLink.InterestId
                                      select rt.Id)
                                      .FirstOrDefaultAsync();

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var newLink = new LinkRT()
                {
                    FK_InterestUserId = userInterestRowId,
                    Link = interestLink.Link
                };
                await context.LinksRelationshipTable.AddAsync(newLink);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }

            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> RemoveUserAsync(int userId)
        {

            //I have a trigger in the database to reset the identity when a user is removed.
            //this is why I have to use ExecuteSqlRawAsync since:
            //var user = await context.Users.FindAsync(userId);
            //"context.Users.Remove(user) did not work.
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.ChangeTracker.Clear();

                    var deleteSql = $"DELETE FROM Users WHERE UserID = {userId}";

                    await context.Database.ExecuteSqlRawAsync(deleteSql);

                    await context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return true;
                }

                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            
        }
    }
}
