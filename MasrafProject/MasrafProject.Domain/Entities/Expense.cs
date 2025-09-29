using MasrafProject.Domain.Abstractions;

public class Expense : Entity
{
    public string MasrafNo { get; set; }=default!;
    public string BelgeNo { get; set; }=default!;
    public DateTime Tarih { get; set; }
    public double ToplamTutar { get; set; }
    public double ToplamKdvTutar { get; set; }
    public double GenelToplam { get; set; }
    public string PicturePath { get; set; } = default!;
    public Guid UserId { get; set; }
    public Guid MuhasebeId { get; set; }
    public Guid MuhasebeOnayId { get; set; }
} 
