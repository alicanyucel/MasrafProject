using MasrafProject.Domain.Abstractions;

namespace MasrafProject.Domain.Entities;

public class ProjectCard : Entity<Guid>
{
    public string ProjeKodu { get; set; }=default!; 
    public string ProjeAdi { get; set; }= default!;
}
