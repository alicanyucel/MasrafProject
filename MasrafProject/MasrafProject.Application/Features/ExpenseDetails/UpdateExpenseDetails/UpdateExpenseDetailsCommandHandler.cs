using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseDetails.UpdateExpenseDetails;

public sealed class UpdateExpenseDetailCommandHandler : IRequestHandler<UpdateExpenseDetailCommand, Result<string>>
{
    private readonly IExpenseDetailRepository _expenseDetailRepo;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateExpenseDetailCommandHandler(IExpenseDetailRepository expenseDetailRepo, IUnitOfWork unitOfWork)
    {
        _expenseDetailRepo = expenseDetailRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _expenseDetailRepo.GetByExpressionAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return Result<string>.Failure("Masraf kaydı bulunamadı.");

        if (!request.YoneticiOnay)
            return Result<string>.Failure("Yönetici onayı gereklidir.");

        if (!request.MuhasebeOnay)
            return Result<string>.Failure("Muhasebe onayı gereklidir.");

        // Tutar hesaplama
        var araToplam = request.BirimFiyat * request.Miktar;
        var kdvTutar = araToplam * request.KdvOran / 100;
        var toplamTutar = araToplam + kdvTutar;

        // Muhasebe tutarı kontrolü
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

        // Güncelleme
        entity.MasrafId = request.MasrafId;
        entity.Tarih = request.Tarih;
        entity.UserId = request.UserId;
        entity.ProjeId = request.ProjeId;
        entity.HizmetId = request.HizmetId;
        entity.MasrafMerkeziId = request.MasrafMerkeziId;
        entity.ManagerUserId = request.ManagerUserId;
        entity.AccountUserId = request.AccountUserId;
        entity.Miktar = request.Miktar;
        entity.BirimFiyat = request.BirimFiyat;
        entity.KdvOran = request.KdvOran;
        entity.Tutar = kabulEdilenTutar;
        entity.SatirAciklama = request.SatirAciklama;
        entity.YoneticiOnay = request.YoneticiOnay;
        entity.YoneticiTutar = request.YoneticiTutar;
        entity.YoneticiAciklama = request.YoneticiAciklama;
        entity.MuhasebeOnay = request.MuhasebeOnay;
        entity.MuhasebeTutar = request.MuhasebeTutar;
        entity.MuhasebeAciklama = request.MuhasebeAciklama;
        entity.LogoAktarim = request.LogoAktarim;

        _expenseDetailRepo.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var mesaj = borcTutar > 0
            ? $"Masraf kaydı güncellendi. Muhasebe onay tutarı aşıldı, {borcTutar:N2} ₺ borç olarak kaydedildi."
            : $"Masraf kaydı güncellendi. Nihai tutar (KDV dahil): {toplamTutar:N2} ₺, muhasebe tarafından onaylandı.";

        return Result<string>.Succeed(mesaj);
    }
}
