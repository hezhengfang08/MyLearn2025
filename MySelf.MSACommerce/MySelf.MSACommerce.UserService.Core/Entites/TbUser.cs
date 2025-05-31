

using MySelf.MSACommerce.SharedKernel.Domain;

namespace MySelf.MSACommerce.UserService.Core.Entites
{
    public class TbUser:BaseAuditEntity,IAggregateRoot
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public string Salt { get; set; } = null!;
    }
}
