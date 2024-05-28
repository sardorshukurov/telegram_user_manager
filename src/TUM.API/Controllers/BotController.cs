using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUM.Application.DTO;
using TUM.Application.Services.BotService;

namespace TUM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly IBotService _service;

        public BotController(IBotService service)
        {
            _service = service;
        }

        [HttpGet("GetAllBots/{adminId}")]
        public async Task<ActionResult<IEnumerable<BotDto>>> GetAllBots(long adminId)
        {
            try
            {
                return Ok(await _service.GetAllAsync(adminId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser(long adminId, Guid botId, AddUserDto user, bool isAdmin)
        {
            try
            {
                await _service.AddUserAsync(adminId, botId, user, isAdmin);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }
        
        [HttpDelete("removeUser/{userId}")]
        public async Task<IActionResult> RemoveUser(long userId, long adminId, Guid botId, bool isAdmin)
        {
            try
            {
                await _service.RemoveUserAsync(adminId, botId, userId, isAdmin);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("changeBanStatus/{userId}")]
        public async Task<IActionResult> ChangeBanStatus(long userId, long adminId, Guid botId, bool ban)
        {
            try
            {
                await _service.ChangeBanStatusAsync(adminId, botId, userId, ban);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("addBot")]
        public async Task<IActionResult> AddBot(CreateBotDto bot)
        {
            try
            {
                await _service.AddBotAsync(bot);
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
