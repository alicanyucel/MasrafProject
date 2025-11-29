using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.ExpenseDetails.UpdateExpenseDetails;

public sealed class UpdateExpenseDetailCommandHandler : IRequestHandler<UpdateExpenseDetailCommand, Result<string>>
{
    private readonly IExpenseDetailRepository _expenseDetailRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITenantProvider _tenantProvider;

    public UpdateExpenseDetailCommandHandler(
        IExpenseDetailRepository expenseDetailRepo,
        IUnitOfWork unitOfWork,
        ITenantProvider tenantProvider)
    {
        _expenseDetailRepo = expenseDetailRepo;
        _unitOfWork = unitOfWork;
        _tenantProvider = tenantProvider;
    }

    public async Task<Result<string>> Handle(UpdateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _expenseDetailRepo.GetByExpressionAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return Result<string>.Failure("Masraf kaydý bulunamadý.");

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

        entity.TenantId = _tenantProvider.TenantId;
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
        entity.BorcTutar = borcTutar;
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
            ? $"Masraf kaydý güncellendi. Muhasebe onay tutarý aþýldý, {borcTutar:N2} ? borç olarak kaydedildi."
            : $"Masraf kaydý güncellendi. Nihai tutar (KDV dahil): {toplamTutar:N2} ?, muhasebe tarafýndan onaylandý.";

        return Result<string>.Succeed(mesaj);
    }
}
