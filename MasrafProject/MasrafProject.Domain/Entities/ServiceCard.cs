using MasrafProject.Domain.Abstractions;

namespace MasrafProject.Domain.Entities;

public class ServiceCard : Entity
{ 
    public string HizmetKodu { get; set; }=default!;
    public string HizmetAdi { get; set; } = default!;
    public double KdvOrani { get; set; }=default!;
}

