using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.CategoryService.UseCases
{
    public record CategoryDto(long Id, string Name);

    public record SpecKeyDto(long Id, string Name);

    public record ParameterKeyDto(long Id, string Name);

    public record ParameterGroupDto
    {
        public long Id { get; init; }
        public string Name { get; init; } = null!;
        public IEnumerable<ParameterKeyDto> ParameterKeysDto { get; init; }
    }

}
