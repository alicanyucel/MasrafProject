using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ExpenseDetails.CreateExpenseDetails;

public sealed record CreateExpenseDetailCommand(
 Guid MasrafId,
 DateTime Tarih,
 Guid UserId,
 Guid ProjeId,
 Guid HizmetId,
 Guid MasrafMerkeziId,
 Guid ManagerUserId,
 Guid AccountUserId,
 decimal Miktar,
 decimal BirimFiyat,
 decimal KdvOran,
 string SatirAciklama,
 bool YoneticiOnay,
 decimal YoneticiTutar,
 string YoneticiAciklama,
 bool MuhasebeOnay,
 decimal MuhasebeTutar,
 string MuhasebeAciklama,
 bool LogoAktarim
):IRequest<Result<string>>;