using MasrafProject.Domain.Abstractions;

namespace MasrafProject.Domain.Entities;

public sealed class ApprovalStatus:Entity
{
    public string Onay { get; set; }=default!;

}
