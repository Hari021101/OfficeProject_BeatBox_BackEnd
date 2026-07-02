using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IChatBotService
    {
        Task<ChatResponseDto> ProcessQueryAsync(ChatQueryDto query);
    }
}
