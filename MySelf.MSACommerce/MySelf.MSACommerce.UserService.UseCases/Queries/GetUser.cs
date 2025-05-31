using MySelf.MSACommerce.UserService.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.UserService.UseCases.Queries
{
    public record UserDto(long Id, string UserName,string? Phone);
    public record GetUserQuery(string UserName, string Password): IQuery<Result<UserDto>>;
    public class GetUserQueryValidator:AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator() 
        {
            RuleFor(query => query.UserName)
                .NotEmpty();
            RuleFor(query => query.Password)
                .NotEmpty();
        }
    }
    public class GetUserQueryHandler(UserDbContext dbContext, IMapper mapper) : IQueryHandler<GetUserQuery, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await dbContext.TbUsers.AsNoTracking()
                           .FirstOrDefaultAsync(tbUser => tbUser.UserName == request.UserName, cancellationToken: cancellationToken);
            if (user == null)
            {
                return Result.NotFound();
            }
            if (Md5Helper.MD5EncodingWithSalt(request.Password, user.Salt) != user.Password)
            {
                return Result.Failure("密码不正确");
            }
            var userDto = mapper.Map<UserDto>(user);

            return Result.Success(userDto);
        }
    }
}
