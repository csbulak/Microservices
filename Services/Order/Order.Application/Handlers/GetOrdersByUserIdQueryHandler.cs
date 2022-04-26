﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Dtos;
using Order.Application.Mapping;
using Order.Application.Queries;
using Order.Infrastructure;
using Shared.Dtos;

namespace Order.Application.Handlers;

public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
{
    private readonly OrderDbContext _dbContext;

    public GetOrdersByUserIdQueryHandler(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _dbContext.Orders.Include(x => x.OrderItems)
            .Where(x => x.BuyerId == request.UserId)
            .ToListAsync(cancellationToken: cancellationToken);

        if (!orders.Any())
        {
            return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
        }

        var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

        return Response<List<OrderDto>>.Success(ordersDto, 200);
    }
}