using MasrafProject.Application.Features.ExpenseDetails.CreateExpenseDetails;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

public sealed class CreateExpenseDetailCommandHandler : IRequestHandler<CreateExpenseDetailCommand, Result<string>>
{
    private readonly IExpenseDetailRepository _expenseDetailRepo;

    public CreateExpenseDetailCommandHandler(IExpenseDetailRepository expenseDetailRepo)
    {
        _expenseDetailRepo = expenseDetailRepo;
    }

    public async Task<Result<string>> Handle(CreateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        if (!request.YoneticiOnay)
            return Result<string>.Failure("Yönetici onayı gereklidir.");

        if (!request.MuhasebeOnay)
            return Result<string>.Failure("Muhasebe onayı gereklidir.");

        // Nihai tutar hesaplama
        var araToplam = request.BirimFiyat * request.Miktar;
        var kdvTutar = araToplam * request.KdvOran / 100;
        var toplamTutar = araToplam + kdvTutar;

        // Yönetici tutarı kontrolü
        decimal borcTutar = 0;
        decimal kabulEdilenTutar;

        if (toplamTutar <= request.YoneticiTutar)
        {
            kabulEdilenTutar = toplamTutar;
            borcTutar = 0;
        }
        else
        {
            kabulEdilenTutar = request.YoneticiTutar;
            borcTutar = toplamTutar - request.YoneticiTutar;
        }

        var entity = new ExpenseDetail
        {
            Id = Guid.NewGuid(),
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
            ? $"Masraf kaydı oluşturuldu. Yönetici tutarı aşıldı, {borcTutar:N2} ₺ borç olarak kaydedildi."
            : $"Masraf kaydı başarıyla oluşturuldu. Nihai tutar (KDV dahil): {toplamTutar:N2} ₺";

        return Result<string>.Succeed(mesaj);
    }
}
