using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using TS.Result;

namespace MasrafProject.Application.Features.Expenses.UpdateExpense;

public sealed record UpdateExpenseCommand(
    Guid Id,
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
    Guid MuhasebeId
) : IRequest<Result<string>>;
