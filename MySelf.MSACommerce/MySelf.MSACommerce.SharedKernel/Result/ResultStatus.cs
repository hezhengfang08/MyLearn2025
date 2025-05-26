using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedKernel.Result
{
    public enum ResultStatus
    {
        Ok,
        Error,
        Forbidden,
        Unauthorized,
        NotFound,
        Invalid
    }
}
