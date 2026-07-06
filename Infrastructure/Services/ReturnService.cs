using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ReturnService : IReturnService
{
    private readonly AppDbContext _context;

    public ReturnService(AppDbContext context)
    {
        _context = context;
    }

    private static ReturnRequestDto ToDto(ReturnRequest x) => new ReturnRequestDto
    {
        Id = x.Id,
        OrderId = x.OrderId,
        UserId = x.UserId,
        ProductId = x.ProductId,
        ProductName = x.Product?.Name ?? "Unknown Product",
        CustomerName = x.User?.FullName ?? "Unknown User",
        Reason = x.Reason,
        Description = x.Description,
        ImageUrls = x.ImageUrls,
        PreferredResolution = x.PreferredResolution,
        Status = x.Status,
        AdminNotes = x.AdminNotes,
        RequestDate = x.RequestDate,
        ProcessedDate = x.ProcessedDate
    };

    public async Task<IEnumerable<ReturnRequestDto>> GetAllRequestsAsync()
    {
        var requests = await _context.ReturnRequests
            .Include(x => x.Order)
            .Include(x => x.User)
            .Include(x => x.Product)
            .OrderByDescending(x => x.RequestDate)
            .ToListAsync();

        return requests.Select(ToDto);
    }

    public async Task<ReturnRequestDto?> GetByOrderIdAsync(int orderId)
    {
        var request = await _context.ReturnRequests
            .Include(x => x.Product)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.OrderId == orderId);

        return request == null ? null : ToDto(request);
    }

    public async Task<ReturnRequestDto> CreateRequestAsync(ReturnRequestDto dto)
    {
        var request = new ReturnRequest
        {
            OrderId = dto.OrderId,
            UserId = dto.UserId,
            ProductId = dto.ProductId,
            Reason = dto.Reason,
            Description = dto.Description,
            ImageUrls = dto.ImageUrls,
            PreferredResolution = dto.PreferredResolution,
            Status = "Pending Approval"
        };

        _context.ReturnRequests.Add(request);
        await _context.SaveChangesAsync();

        dto.Id = request.Id;
        dto.Status = request.Status;
        dto.RequestDate = request.RequestDate;
        return dto;
    }

    public async Task<ReturnRequestDto> UpdateRequestStatusAsync(Guid id, string status, string? adminNotes)
    {
        var request = await _context.ReturnRequests.FindAsync(id);
        if (request == null) throw new Exception("Return request not found");

        request.Status = status;
        if (adminNotes != null) request.AdminNotes = adminNotes;
        request.ProcessedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Re-load with navigations for full DTO
        await _context.Entry(request).Reference(x => x.Product).LoadAsync();
        await _context.Entry(request).Reference(x => x.User).LoadAsync();

        return ToDto(request);
    }
}
