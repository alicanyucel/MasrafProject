using MasrafProject.Domain.Abstractions;

namespace MasrafProject.Domain.Entities;
public class ExpenseDetail : Entity
{
    public Guid MasrafId { get; set; }
    public DateTime Tarih { get; set; }
    public Guid UserId { get; set; }
    public Guid ProjeId { get; set; }
    public Guid HizmetId { get; set; }
    public Guid MasrafMerkeziId { get; set; }
    public Guid ManagerUserId { get; set; }
    public Guid AccountUserId { get; set; }
    public double Miktar { get; set; }
    public double BirimFiyat { get; set; }
    public double Tutar { get; set; }
    public double KdvOran { get; set; } = default!;
    public string SatirAciklama { get; set; }=default!;
    public int YoneticiOnay { get; set; }
    public double YoneticiTutar { get; set; }
    public string YoneticiAciklama { get; set; }=default!;
    public int MuhasebeOnay { get; set; }
    public double MuhasebeTutar { get; set; }
    public string MuhasebeAciklama { get; set; }=default!;
    public bool LogoAktarim { get; set; }=false;    
}
