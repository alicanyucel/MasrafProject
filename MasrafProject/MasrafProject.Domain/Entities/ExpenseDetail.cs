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
    public decimal Miktar { get; set; }
    public decimal BirimFiyat { get; set; }
    public decimal Tutar { get; set; }
    public decimal KdvOran { get; set; } = default!;
    public string SatirAciklama { get; set; }=default!;
    public bool YoneticiOnay { get; set; }
    public decimal YoneticiTutar { get; set; }
    public string YoneticiAciklama { get; set; }=default!;
    public bool MuhasebeOnay { get; set; }
    public decimal MuhasebeTutar { get; set; }
    public string MuhasebeAciklama { get; set; }=default!;
    public bool LogoAktarim { get; set; }=false;    
}
