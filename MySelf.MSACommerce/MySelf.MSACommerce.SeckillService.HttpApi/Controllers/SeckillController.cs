using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.MSACommerce.HttpApi.Common.Infrastructure;
using MySelf.MSACommerce.SeckillService.Infrastructure;
using MySelf.MSACommerce.SeckillService.UseCases;
using MySelf.MSACommerce.SeckillService.UseCases.Commands;
using MySelf.MSACommerce.SeckillService.UseCases.Queries;

namespace MySelf.MSACommerce.SeckillService.HttpApi.Controllers
{
    [Route("api/seckill")]
    [ApiController]
    public class SeckillController() : ApiControllerBase
    {
        [HttpGet("times")]
        public IActionResult GetSecKillBeginTimes()
        {
            var beginTimes = SecKillDate.GetBeginTimes()
                .Select(t => new { time = t.ToSecKillTime(), display = t.ToString("HH:mm") });
            return Ok(beginTimes);
        }

        // 2024120620
        [HttpGet("list/{time}")]
        public async Task<IActionResult> GetSecKillProductsByTime(string time)
        {
            var result = await Sender.Send(new GetSecKillProductsByTimeQuery(time));
            return ReturnResult(result);
        }

        [HttpGet("{time}/{id:long}")]
        public async Task<IActionResult> GetSecKillProductById(string time, long id)
        {
            var result = await Sender.Send(new GetSecKillProductByIdQuery(time, id));
            if (!result.IsSuccess) return ReturnResult(result);
            return Ok(new { product = result.Value, CurrentTime = DateTime.Now });
        }

        [HttpGet("verifyCode")]
        [Authorize]
        public async Task<IActionResult> GetVerifyCode()
        {
            var result = await Sender.Send(new CreateVerifyCodeCommand(5));
            return result.IsSuccess ? File(result.Value, "image/jpeg") : ReturnResult(result);
        }

        [HttpGet("link/{id:long}")]
        [Authorize]
        public async Task<IActionResult> GetSecKillLink(long id, [FromQuery] string verifyCode)
        {
            var verifyResult = await Sender.Send(new GetVerifyCodeQuery(verifyCode));
            if (!verifyResult.IsSuccess) return ReturnResult(verifyResult);

            var linkResult = await Sender.Send(new CreateSecKillLinkCommand(id));
            return ReturnResult(linkResult);
        }

        [HttpPost("order/{link}/{time}/{id:long}")]
        [Authorize]
        public async Task<IActionResult> CreateSecKillOrder(string link, string time, long id,
            [FromServices] MultiThreadingCreateOrder multiThreadingCreateOrder)
        {
            var linkResult = await Sender.Send(new GetSecKillLinkQuery(id, link));
            if (!linkResult.IsSuccess) return ReturnResult(linkResult);

            var orderResult = await Sender.Send(new CreateSecKillOrderCommand(id, link));
            if (!orderResult.IsSuccess) return ReturnResult(orderResult);

            multiThreadingCreateOrder.CreateOrder();
            return Ok();
        }
    }
}
