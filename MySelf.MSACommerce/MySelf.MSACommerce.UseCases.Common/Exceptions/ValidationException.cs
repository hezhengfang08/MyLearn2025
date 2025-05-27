using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;


namespace MySelf.MSACommerce.UseCases.Common.Exceptions
{
    public class ValidationException():Exception("发生了一个或者多个验证失败")
    {

        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
        public ValidationException(IEnumerable<ValidationFailure> failures):this()
        {
            Errors = failures.GroupBy(t => t.PropertyName, t => t.ErrorMessage)
                .ToDictionary(d => d.Key, d => d.ToArray());
        }
    }
}
