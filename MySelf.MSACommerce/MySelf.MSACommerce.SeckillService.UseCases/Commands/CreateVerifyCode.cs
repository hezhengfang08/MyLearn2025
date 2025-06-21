using CaptchaGen.NetCore;
using MySelf.MSACommerce.SeckillService.Core;
using MySelf.MSACommerce.UseCases.Common.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SeckillService.UseCases.Commands
{
    public record CreateVerifyCodeCommand(int Count) : ICommand<Result<MemoryStream>>;

    public class CreateVerifyCodeCommandHandler(IConnectionMultiplexer redis, IUser user) : ICommandHandler<CreateVerifyCodeCommand, Result<MemoryStream>>
    {
        public async Task<Result<MemoryStream>> Handle(CreateVerifyCodeCommand request, CancellationToken cancellationToken)
        {
            var db = redis.GetDatabase();
            var code = ImageFactory.CreateCode(request.Count);
            await db.StringSetAsync($"{RedisKeyConstants.SecKillVerifyCodePrefix}{user.Id}", code, TimeSpan.FromSeconds(60));
            var image = ImageFactory.BuildImage(code, 50, 100, 20, 10, ImageFormat.Jpeg);

            return image is null ? Result.Failure("验证码生成失败") : Result.Success(image);
        }
    }
}
