using MasrafProject.Domain.Abstractions;

namespace MasrafProject.Domain.Entities;
public class ExpenseCenterCard : Entity
{ 
    public string MasrafMerkeziKodu { get; set; }=default!;
    public string MasrafMerkeziAdi { get; set; } = default!;
}
