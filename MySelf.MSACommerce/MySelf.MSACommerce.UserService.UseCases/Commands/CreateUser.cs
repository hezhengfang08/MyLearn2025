

using MySelf.MSACommerce.UserService.Core;
using MySelf.MSACommerce.UserService.Core.Entites;
using MySelf.MSACommerce.UserService.Infrastructure.Tools;

namespace MySelf.MSACommerce.UserService.UseCases.Commands
{
   public record CreateUserCommand(string UserName, string Password,string? Phone ):ICommand<Result>;
    public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.UserName)
                .NotEmpty()
                .MaximumLength(DataSchemaConstants.DefaultUsernameMaxLength);
            RuleFor(command => command.Password)
               .NotEmpty()
               .MinimumLength(6)
               .MaximumLength(DataSchemaConstants.DefaultPasswordMaxLength);
            RuleFor(command => command.Phone)
               .Length(DataSchemaConstants.DefaultPhoneLength);
        }
    }
    public class CreateUserCommonHandler(UserDbContext dbContext, IMapper mapper) : ICommandHandler<CreateUserCommand, Result>
    {
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await dbContext.TbUsers.AnyAsync(user=>user.UserName == request.UserName, cancellationToken:cancellationToken);
            if (userExists) 
            {
                return Result.Failure("用户名已经存在");
            }
            var user = mapper.Map<TbUser>(request);
            user.Salt = user.UserName.ToMD5();
            user.Password = user.Password.ToMD5WithSalt(user.Salt);  
            dbContext.TbUsers.Add(user);
            var count = await dbContext.SaveChangesAsync(cancellationToken);
            return count != 1 ? Result.Failure("用户注册失败") : Result.Success();
        }
    }
}
