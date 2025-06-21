using MySelf.MSACommerce.SeckillService.Core;
using MySelf.MSACommerce.UseCases.Common.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SeckillService.UseCases.Commands
{
    public record CreateSecKillLinkCommand(long Id) : ICommand<Result<string>>;

    public class CreateSecKillLinkCommandHandler(IConnectionMultiplexer redis, IUser user) : ICommandHandler<CreateSecKillLinkCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateSecKillLinkCommand request, CancellationToken cancellationToken)
        {
            var inputBytes = Encoding.UTF8.GetBytes($"{request.Id}{user.Id}");
            var hashBytes = MD5.HashData(inputBytes);
            // 将字节数组转换为十六进制字符串
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            var link = sb.ToString();

            var db = redis.GetDatabase();
            var key = $"{RedisKeyConstants.SecKillLinkPrefix}{user.Id}";
            await db.HashSetAsync(key, request.Id, link);
            db.KeyExpire(key, TimeSpan.FromSeconds(60));
            return Result.Success(link);
        }
    }
}
