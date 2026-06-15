using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces;

public interface IReturnService
{
    Task<IEnumerable<ReturnRequestDto>> GetAllRequestsAsync();
    Task<ReturnRequestDto> CreateRequestAsync(ReturnRequestDto dto);
    Task<ReturnRequestDto> UpdateRequestStatusAsync(Guid id, string status, string? adminNotes);
}
