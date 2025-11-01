using System;

namespace MasrafProject.Domain.Abstractions;

public abstract class Entity<TId> where TId : struct
{
    public bool IsDeleted { get; set; } = false;
    public int TenantId { get; set; }
    public TId Id { get; set; }

    protected Entity()
    {
        if (typeof(TId) == typeof(Guid))
        {
            Id = (TId)(object)Guid.NewGuid();
        }
    }
}

public abstract class Entity : Entity<Guid>
{
    protected Entity() : base() { }
}

