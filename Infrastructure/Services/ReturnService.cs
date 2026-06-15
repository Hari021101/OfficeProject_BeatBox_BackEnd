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

    public async Task<IEnumerable<ReturnRequestDto>> GetAllRequestsAsync()
    {
        var requests = await _context.ReturnRequests
            .Include(x => x.Order)
            .Include(x => x.User)
            .Include(x => x.Product)
            .OrderByDescending(x => x.RequestDate)
            .ToListAsync();

        return requests.Select(x => new ReturnRequestDto
        {
            Id = x.Id,
            OrderId = x.OrderId,
            UserId = x.UserId,
            ProductId = x.ProductId,
            ProductName = x.Product?.Name ?? "Unknown Product",
            CustomerName = x.User?.FullName ?? "Unknown User",
            Reason = x.Reason,
            Status = x.Status,
            AdminNotes = x.AdminNotes,
            RequestDate = x.RequestDate,
            ProcessedDate = x.ProcessedDate
        });
    }

    public async Task<ReturnRequestDto> CreateRequestAsync(ReturnRequestDto dto)
    {
        var request = new ReturnRequest
        {
            OrderId = dto.OrderId,
            UserId = dto.UserId,
            ProductId = dto.ProductId,
            Reason = dto.Reason,
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
        request.AdminNotes = adminNotes;
        request.ProcessedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return new ReturnRequestDto
        {
            Id = request.Id,
            OrderId = request.OrderId,
            UserId = request.UserId,
            ProductId = request.ProductId,
            Reason = request.Reason,
            Status = request.Status,
            AdminNotes = request.AdminNotes,
            RequestDate = request.RequestDate,
            ProcessedDate = request.ProcessedDate
        };
    }
}
