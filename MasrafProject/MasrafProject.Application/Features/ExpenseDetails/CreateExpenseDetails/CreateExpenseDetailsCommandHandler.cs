using MasrafProject.Application.Features.ExpenseDetails.CreateExpenseDetails;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

public sealed class CreateExpenseDetailCommandHandler : IRequestHandler<CreateExpenseDetailCommand, Result<string>>
{
    private readonly IExpenseDetailRepository _expenseDetailRepo;
    private readonly ITenantProvider _tenantProvider;

    public CreateExpenseDetailCommandHandler(IExpenseDetailRepository expenseDetailRepo, ITenantProvider tenantProvider)
    {
        _expenseDetailRepo = expenseDetailRepo;
        _tenantProvider = tenantProvider;
    }

    public async Task<Result<string>> Handle(CreateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        if (!request.YoneticiOnay)
            return Result<string>.Failure("Yönetici onayý gereklidir.");
        if (!request.MuhasebeOnay)
            return Result<string>.Failure("Muhasebe onayý gereklidir.");
            
        var araToplam = request.BirimFiyat * request.Miktar;
        var kdvTutar = araToplam * request.KdvOran / 100;
        var toplamTutar = araToplam + kdvTutar;
        decimal borcTutar = 0;
        decimal kabulEdilenTutar;

        if (toplamTutar <= request.MuhasebeTutar)
        {
            kabulEdilenTutar = toplamTutar;
            borcTutar = 0;
        }
        else
        {
            kabulEdilenTutar = request.MuhasebeTutar;
            borcTutar = toplamTutar - request.MuhasebeTutar;
        }

        var entity = new ExpenseDetail
        {
            Id = Guid.NewGuid(),
            TenantId = _tenantProvider.TenantId,
            MasrafId = request.MasrafId,
            Tarih = request.Tarih,
            UserId = request.UserId,
            ProjeId = request.ProjeId,
            HizmetId = request.HizmetId,
            MasrafMerkeziId = request.MasrafMerkeziId,
            ManagerUserId = request.ManagerUserId,
            AccountUserId = request.AccountUserId,
            Miktar = request.Miktar,
            BirimFiyat = request.BirimFiyat,
            KdvOran = request.KdvOran,
            Tutar = kabulEdilenTutar,
            BorcTutar = borcTutar, 
            SatirAciklama = request.SatirAciklama,
            YoneticiOnay = request.YoneticiOnay,
            YoneticiTutar = request.YoneticiTutar,
            YoneticiAciklama = request.YoneticiAciklama,
            MuhasebeOnay = request.MuhasebeOnay,
            MuhasebeTutar = request.MuhasebeTutar,
            MuhasebeAciklama = request.MuhasebeAciklama,
            LogoAktarim = request.LogoAktarim
        };

        await _expenseDetailRepo.AddAsync(entity, cancellationToken);

        var mesaj = borcTutar > 0
            ? $"Masraf kaydý baþarýyla oluþturuldu. Muhasebe onay tutarý aþýldý, {borcTutar:N2} ? borç olarak kaydedildi."
            : $"Masraf kaydý baþarýyla oluþturuldu. Nihai tutar (KDV dahil): {toplamTutar:N2} ?, muhasebe tarafýndan onaylandý.";

        return Result<string>.Succeed(mesaj);
    }
}
