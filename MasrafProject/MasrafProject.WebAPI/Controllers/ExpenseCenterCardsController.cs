using MasrafProject.Application.Features.ExpenseCenterCards.CreateExpenseCenterCard;
using MasrafProject.Application.Features.ExpenseCenterCards.DeleteExpenseCenterCard;
using MasrafProject.Application.Features.ExpenseCenterCards.GetAllExpenseCenterCard;
using MasrafProject.Application.Features.ExpenseCenterCards.GetByIdExpenseCenterCard;
using MasrafProject.Application.Features.ExpenseCenterCards.UpdateCenterCard;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers;
[AllowAnonymous]
public sealed class ExpenseCenterCardsController : ApiController
{
    public ExpenseCenterCardsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateExpenseCenterCard(CreateExpenseCenterCardCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Ekleme işlemi başarılı." })
        : BadRequest(new { message = "Ekleme işlemi başarısız." });
    }

    [HttpPost]
    public async Task<IActionResult> ExpenseCenterCardGetById(GetExpenseCenterCardByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> ExpenseCenterCardDelete(DeleteExpenseCenterCardCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllExpenseCenterCard(GetAllExpenseCenterCardQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateExpenseCenterCard(UpdateExpenseCenterCardCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Güncelleme işlemi başarılı." })
        : BadRequest(new { message = "Güncelleme işlemi başarısız." });
    }
}
