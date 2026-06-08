namespace ETicaret.Domain.Entities.Common
{
    public class BaseEntity : IEntityBase
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual bool IsDeleted { get; set; } = false;
    }
}
