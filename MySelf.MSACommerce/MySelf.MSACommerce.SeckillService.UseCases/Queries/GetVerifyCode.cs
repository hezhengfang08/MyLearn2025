using MySelf.MSACommerce.SeckillService.Core;
using MySelf.MSACommerce.UseCases.Common.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SeckillService.UseCases.Queries
{
    public record GetVerifyCodeQuery(string Code) : IQuery<Result>;

    public class GetVerifyCodeQueryHandler(IConnectionMultiplexer redis, IUser user) : IQueryHandler<GetVerifyCodeQuery, Result>
    {
        public async Task<Result> Handle(GetVerifyCodeQuery request, CancellationToken cancellationToken)
        {
            var db = redis.GetDatabase();
            var code = await db.StringGetDeleteAsync($"{RedisKeyConstants.SecKillVerifyCodePrefix}{user.Id}");
            if (code.IsNullOrEmpty) return Result.Failure("验证码已过期");
            return code != request.Code ? Result.Failure("验证码错误") : Result.Success();
        }
    }
}
