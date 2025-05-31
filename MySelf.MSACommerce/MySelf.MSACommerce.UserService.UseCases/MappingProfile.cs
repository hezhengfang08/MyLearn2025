using MySelf.MSACommerce.UserService.Core.Entites;
using MySelf.MSACommerce.UserService.UseCases.Commands;
using MySelf.MSACommerce.UserService.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.UserService.UseCases
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<CreateUserCommand, TbUser>();
            CreateMap<TbUser, UserDto>();
        }
    }
}
