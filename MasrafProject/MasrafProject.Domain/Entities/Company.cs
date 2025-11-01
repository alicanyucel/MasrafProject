using MasrafProject.Domain.Abstractions;

namespace MasrafProject.Domain.Entities;

public sealed class Company: Entity<int>
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;

}
