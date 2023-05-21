using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkolprojektLab3.Models.DTO;
using SkolprojektLab3.Repos.IRepos;
using System.ComponentModel.DataAnnotations;

namespace SkolprojektLab3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGetInfoRepository getUserRepo;

        private readonly IPostDataRepository postDataRepo;

        public UserController(IGetInfoRepository _getUserRepo, IPostDataRepository _postDataRepo)
        {
            getUserRepo = _getUserRepo;
            postDataRepo = _postDataRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserDto>>> GetUsers(string filter = "")
        {
            var allUsers = await getUserRepo.GetAllUsersAsync(filter);
            if(allUsers.Count == 0)
            {
                return NotFound("User was not found");
            }

            return Ok(allUsers);
        }

        [HttpGet]
        [Route("GetUserInterests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserInterestsDto>>> GetUserInterests([Required] int userId)
        {
            int usersAmount = await getUserRepo.UserCountAsync();

            if(userId > usersAmount || userId <= 0)
            {
                return NotFound($"No user with Id {userId} exists.");
            }

            var allUserInterestsList = await getUserRepo.GetInterestsByUserIdAsync(userId);

            return Ok(allUserInterestsList);
        }

        [HttpGet]
        [Route("GetUserLinks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<UserLinksDto>>> GetUserLinks([Required] int userId)
        {
            int usersAmount = await getUserRepo.UserCountAsync();

            if (userId > usersAmount || userId <= 0)
            {
                return NotFound($"No user with Id {userId} exists.");
            }

            var allUserLinksList = await getUserRepo.GetUserLinksAsync(userId);

            return Ok(allUserLinksList);
        }

        [HttpGet]
        [Route("GetUserDataById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<GetAllUserDataDto>>> GetUserData([Required] int userId)
        {
            int usersAmount = await getUserRepo.UserCountAsync();

            if (userId > usersAmount || userId <= 0)
            {
                return NotFound($"No user with Id {userId} exists.");
            }

            var allData = await getUserRepo.GetAllUsersDataAsync(userId);

            return Ok(allData);
        }

        [HttpPost]
        [Route("AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddNewUser([FromBody, Required] NewUserDto newUser)
        {
            if(!string.IsNullOrEmpty(newUser.PhoneNumber) && !string.IsNullOrEmpty(newUser.FirstName) && !string.IsNullOrEmpty(newUser.LastName))
            {
                if (newUser.FirstName.ToLower() != "string" || newUser.LastName.ToLower() != "string" || newUser.PhoneNumber.ToLower() != "string")
                {
                    var result = await postDataRepo.AddNewUserAsync(newUser);

                    if (result)
                    {
                        return StatusCode(StatusCodes.Status201Created, "User has been added successfully!");
                    }

                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return BadRequest("Can not use the value 'string'.");
            }

            return BadRequest("Error. The requested data was not complete.");
        }

        [HttpPost]
        [Route("AddNewInterest")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddInterestToUser([FromBody, Required] AddInterestToUserDto userInterest)
        {
            int totalInterestCount = await getUserRepo.UserCountAsync();

            int totalUserCount = await getUserRepo.InterestCountAsync();

            if (userInterest.UserId <= totalUserCount && userInterest.UserId !<= 0)
            {
                if (!string.IsNullOrEmpty(userInterest.InterestTitle) && userInterest.InterestTitle != "string")
                {
                    var result = await postDataRepo.AddnConnectInterestToUserAsync(userInterest);

                    if(result == true)
                    {
                        return StatusCode(StatusCodes.Status201Created, "Interest was added successfully!");
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return BadRequest("Interest title was invalid.");
            }
            return NotFound($"{userInterest.UserId} does not exist.");
        }

        [HttpPost]
        [Route("AddNewLink")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddLinkToInterest([FromBody, Required] AddInterestLinkDto interestLink)
        {
            int totalInterestCount = await getUserRepo.UserCountAsync();

            int totalUserCount = await getUserRepo.InterestCountAsync();

            if (interestLink.UserId <= totalUserCount && interestLink.UserId !<= 0 && interestLink.InterestId <= totalInterestCount && interestLink.InterestId !<= 0 )
            {
                if (!string.IsNullOrEmpty(interestLink.Link) && interestLink.Link != "string")
                {
                    var result = await postDataRepo.AddLinkToInterestAsync(interestLink);

                    if (result)
                    {
                        return StatusCode(StatusCodes.Status201Created, "The link has been added successfully!");
                    }

                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                return BadRequest("Value not allowed. Try again.");
                
            }

            return NotFound($"{interestLink.UserId} does not exist.");
        }

        [HttpDelete]
        [Route("RemoveUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RemoveUser([Required] int userId)
        {
            bool userExists = await getUserRepo.CheckUserExistsAsync(userId);

            if (userExists)
            {
                bool userRemoved = await postDataRepo.RemoveUserAsync(userId);

                if (userRemoved)
                {
                    return Ok($"User with Id '{userId}' was removed.");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Error, user was not removed.");
            }

            return NotFound();
        }
    }
}
