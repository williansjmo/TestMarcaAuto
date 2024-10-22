using System;

namespace Test.Domain.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}
