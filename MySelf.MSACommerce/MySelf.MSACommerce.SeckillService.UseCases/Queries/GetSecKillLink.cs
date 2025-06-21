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
    public record GetSecKillLinkQuery(long Id, string Link) : ICommand<Result>;

    public class GetSecKillLinkQueryHandler(IConnectionMultiplexer redis, IUser user) : ICommandHandler<GetSecKillLinkQuery, Result>
    {
        public async Task<Result> Handle(GetSecKillLinkQuery request, CancellationToken cancellationToken)
        {
            var db = redis.GetDatabase();
            var key = $"{RedisKeyConstants.SecKillLinkPrefix}{user.Id}";
            var link = await db.HashGetAsync(key, request.Id);
            if (link.IsNullOrEmpty) return Result.Failure("秒杀链接已失效");
            await db.HashDeleteAsync(key, request.Id);
            return link != request.Link ? Result.Failure("秒杀链接无效") : Result.Success();
        }
    }
}
