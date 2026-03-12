using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Services.Interfaces;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUser userService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser(CreateUserRequest user)
        {
            var createdUser = await userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest user)
        {
            var updated = await userService.UpdateUserAsync(id, user);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await userService.DeleteUserAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
