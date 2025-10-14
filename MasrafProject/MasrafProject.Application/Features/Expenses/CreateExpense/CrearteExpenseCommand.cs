using MediatR;
using TS.Result;
using Swashbuckle.AspNetCore.Annotations;

namespace MasrafProject.Application.Features.Expenses.CreateExpense;

public sealed record CreateExpenseCommand(
    string MasrafNo,
    string BelgeNo,
    DateTime Tarih,
    decimal ToplamTutar,
    [SwaggerSchema(ReadOnly = true)]
    decimal ToplamKdvTutar,
    [SwaggerSchema(ReadOnly = true)]
    decimal GenelToplam,
    string PicturePath,
    Guid UserId,
    Guid MuhasebeId,
    Guid MuhasebeOnayId
) : IRequest<Result<string>>;
