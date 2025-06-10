using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.UseCases.Common.Interfaces
{
    public interface IUser
    {
        long Id { get; }
        string? UserName { get; }
    }
}
