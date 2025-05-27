using Microsoft.AspNetCore.Http;
using MySelf.MSACommerce.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.HttpApi.Common.Services
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
    {
        private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

        public int? Id
        {
            get
            {
                var id = _user?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (id is null) return null;

                return Convert.ToInt32(id);
            }
        }

        public string? UserName => _user?.FindFirstValue(ClaimTypes.Name);
    }
}
