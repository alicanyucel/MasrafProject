using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.UpdateExpense;

public sealed record UpdateExpenseCommand(
Guid Id,
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
