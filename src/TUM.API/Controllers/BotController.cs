using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TUM.Application.DTO;
using TUM.Application.Services.BotService;

namespace TUM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBotService _service;

        public BotController(IBotService service)
        {
            _service = service;
        }

        [HttpGet(nameof(adminId))]
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

        [HttpPost]
        [Route("/addUser")]
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
        
        [HttpDelete(nameof(userId))]
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

        [HttpPut(nameof(userId))]
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

        [HttpPost]
        [Route("/addBot")]
        public async Task<IActionResult> AddBot(CreateBotDto bot, long adminId)
        {
            try
            {
                await _service.AddBotAsync(bot, adminId);
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
