using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.CreateExpense;

public sealed record CreateExpenseCommand(
 string MasrafNo,
 string BelgeNo,
 DateTime Tarih,
 decimal ToplamTutar,
 decimal ToplamKdvTutar,
 decimal GenelToplam,
 string PicturePath,
 Guid UserId,
 Guid MuhasebeId,
 Guid MuhasebeOnayId
) : IRequest<Result<string>>;
