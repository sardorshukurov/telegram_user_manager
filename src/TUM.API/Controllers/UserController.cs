using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUM.Application.DTO;
using TUM.Application.Services.UserService;

namespace TUM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll(Guid botId)
        {
            try
            {
                return Ok(await _service.GetAllAsync(botId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPut(nameof(userId))]
        public async Task<IActionResult> UpdateUserActivity(long userId)
        {
            try
            {
                await _service.UpdateUserActivity(userId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }
    }
}
