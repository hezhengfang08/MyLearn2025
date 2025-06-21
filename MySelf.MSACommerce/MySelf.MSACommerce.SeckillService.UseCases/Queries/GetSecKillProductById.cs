using MySelf.MSACommerce.SeckillService.Core;
using MySelf.MSACommerce.SeckillService.Core.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SeckillService.UseCases.Queries
{
    public record GetSecKillProductByIdQuery(string Time, long Id) : IQuery<Result<SecKillProduct>>;

    public class GetSecKillProductByIdQueryValidator : AbstractValidator<GetSecKillProductByIdQuery>
    {
        public GetSecKillProductByIdQueryValidator()
        {
            RuleFor(query => query.Time)
                .NotEmpty();
        }
    }

    public class GetSecKillProductByIdQueryQueryHandler(IConnectionMultiplexer redis) : IQueryHandler<GetSecKillProductByIdQuery, Result<SecKillProduct>>
    {
        public async Task<Result<SecKillProduct>> Handle(GetSecKillProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var db = redis.GetDatabase();
            var secKillValue = await db.HashGetAsync($"{RedisKeyConstants.SeckillDatePrefix}{request.Time}", request.Id);
            if (secKillValue.HasValue == false || secKillValue.IsNull) return Result.NotFound();
            var secKill = JsonConvert.DeserializeObject<SecKillProduct>(secKillValue!);

            return Result.Success(secKill!);
        }
    }
}
