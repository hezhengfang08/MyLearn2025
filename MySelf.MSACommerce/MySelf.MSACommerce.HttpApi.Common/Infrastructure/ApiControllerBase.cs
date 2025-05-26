using MySelf.MSACommerce.HttpApi.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MySelf.MSACommerce.SharedKernel.Result;


namespace MySelf.MSACommerce.HttpApi.Common.Infrastructure
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ISender Sender => HttpContent.RequestService.GetRequestService<ISender>();

        protected IActionResult ReturnResult(IResult result)
        {
            switch (result.Status)
            {
                case ResultStatus.Ok:
                    {
                        var value = result.GetValue();
                        return value is null ? NoContent() : Ok(value);
                    }
                case ResultStatus.Error:
                    {
                        return result.Errors is null ? BadRequest() : BadRequest(new { errors = result.Errors });
                    }
                case ResultStatus.NotFound:
                    {
                        return result.Errors is null ? NotFound() : NotFound(new { errors = result.Errors });
                    }
                case ResultStatus.Invalid:
                    return result.Errors is null ? BadRequest() : BadRequest(new { errors = result.Errors });

                case ResultStatus.Forbidden:
                    return StatusCode(403);

                case ResultStatus.Unauthorized:
                    return Unauthorized();

                default:
                    return BadRequest(new { errors = result.Errors });

            }
        }
    }
}
