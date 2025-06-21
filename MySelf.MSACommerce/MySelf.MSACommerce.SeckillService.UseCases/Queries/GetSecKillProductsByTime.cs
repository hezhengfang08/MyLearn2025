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
    public record GetSecKillProductsByTimeQuery(string Time) : IQuery<Result<List<SecKillProduct>>>;

    public class GetSecKillProductsByTimeQueryValidator : AbstractValidator<GetSecKillProductsByTimeQuery>
    {
        public GetSecKillProductsByTimeQueryValidator()
        {
            RuleFor(query => query.Time)
                .NotEmpty();
        }
    }

    public class GetSecKillProductsByTimeQueryQueryHandler(IConnectionMultiplexer redis) : IQueryHandler<GetSecKillProductsByTimeQuery, Result<List<SecKillProduct>>>
    {
        public async Task<Result<List<SecKillProduct>>> Handle(GetSecKillProductsByTimeQuery request,
            CancellationToken cancellationToken)
        {
            var db = redis.GetDatabase();
            var secKillValues = await db.HashValuesAsync($"{RedisKeyConstants.SeckillDatePrefix}{request.Time}");
            if (secKillValues.Length == 0) return Result.NotFound();

            var secKills = secKillValues
                .Select(secKillValue => JsonConvert.DeserializeObject<SecKillProduct>(secKillValue.ToString()))
                .OfType<SecKillProduct>()
                .ToList();

            return secKills.Count > 0 ? Result.Success(secKills) : Result.NotFound();
        }
    }
}
