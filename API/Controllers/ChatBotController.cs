using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatBotController : ControllerBase
    {
        private readonly IChatBotService _chatBotService;

        public ChatBotController(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        [HttpPost("query")]
        [AllowAnonymous] // Allow guests to use the chatbot
        public async Task<IActionResult> Query([FromBody] ChatQueryDto query)
        {
            var response = await _chatBotService.ProcessQueryAsync(query);
            return Ok(response);
        }
    }
}
