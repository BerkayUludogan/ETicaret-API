using ETicaret.Domain.Entities.Common;
using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Domain.Entities.Basket
{
    public class BasketEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUserEntity User { get; set; } = default!;
        public ICollection<BasketItemEntity> Items { get; set; } = [];
    }
}
