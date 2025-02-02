using BlazorUserApp.Server.DTOs;
using BlazorUserApp.Server.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlazorUserApp.Server.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UserReadModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<UserReadModel>>> GetUsers()
        {
            return Ok(await userRepository.GetUsersAsync());
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserReadModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserReadModel>> GetUser(int userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserWriteModel userWriteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await userRepository.AddUserAsync(userWriteModel);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserWriteModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await userRepository.UpdateUserAsync(userId, userModel);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await userRepository.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}