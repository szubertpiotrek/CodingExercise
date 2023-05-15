using Domain.Base;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string? Firstname { get; init; }
        public string? Surname { get; init; }
    }
}