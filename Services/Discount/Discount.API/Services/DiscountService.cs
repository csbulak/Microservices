using System.Data;
using Dapper;
using Npgsql;
using Shared.Dtos;

namespace Discount.API.Services;

public class DiscountService : IDiscountService
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;

    public DiscountService(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
    }
    public async Task<Response<List<Models.Discount>>> GetAll()
    {
        var discount = await _connection.QueryAsync<Models.Discount>("select * from discount");
        return Response<List<Models.Discount>>.Success(discount.ToList(), 200);
    }

    public async Task<Response<Models.Discount>> GetById(int id)
    {
        var discount = (await _connection.QueryAsync<Models.Discount>("select * from discount where id = @id", new
        {
            id = id
        })).SingleOrDefault();

        return discount == null
            ? Response<Models.Discount>.Fail("not found",
                404)
            : Response<Models.Discount>.Success(discount,
                200);
    }

    public async Task<Response<NoContent>> Save(Models.Discount discount)
    {
        var status = await _connection.ExecuteAsync(
            "insert into discount (userid,rate,code) values(@UserId,@Rate,@Code)", discount);

        return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("an error accrued while adding.", 500);
    }

    public async Task<Response<NoContent>> Update(Models.Discount discount)
    {
        var status = await _connection.ExecuteAsync(
            "update discount set userid=@UserId,rate=@Rate,code=@Code where id = @Id", new
            {
                UserId = discount.UserId,
                Rate = discount.Rate,
                Code = discount.Code,
                Id = discount.Id
            });

        return status > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("discount not found", 404);

    }

    public async Task<Response<NoContent>> Delete(int id)
    {
        var status = await _connection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

        return status > 0 ? Response<NoContent>.Success(200) : Response<NoContent>.Fail("discount not found", 404);
    }

    public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
    {
        var discounts = await _connection.QueryAsync<Models.Discount>("select * from discount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });

        var hasDiscount = discounts.FirstOrDefault();

        if (hasDiscount == null)
        {
            return Response<Models.Discount>.Fail("Discount not found", 404);
        }

        return Response<Models.Discount>.Success(hasDiscount, 200);
    }
}