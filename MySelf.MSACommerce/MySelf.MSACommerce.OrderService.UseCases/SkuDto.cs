using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.OrderService.UseCases
{
    public record SkuDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Images { get; set; }
        public long Price { get; set; }
         [JsonConverter(typeof(NonEscapedStringConverter))]
        public dynamic Spec { get; set; } = null!;
    }
}
