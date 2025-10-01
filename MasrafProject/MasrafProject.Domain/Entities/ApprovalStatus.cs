using MasrafProject.Domain.Abstractions;

namespace MasrafProject.Domain.Entities;

public sealed class ApprovalStatus:Entity
{
    public bool Onay { get; set; }=default!;

}
