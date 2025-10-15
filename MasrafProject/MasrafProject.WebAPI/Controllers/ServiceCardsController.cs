using MasrafProject.Application.Features.Auth.Roles.SetUserRoles;
using MasrafProject.Application.Features.ServiceCards.CreateServiceCards;
using MasrafProject.Application.Features.ServiceCards.DeleteServiceCards;
using MasrafProject.Application.Features.ServiceCards.GetAllServiceCards;
using MasrafProject.Application.Features.ServiceCards.GetByIdServiceCards;
using MasrafProject.Application.Features.ServiceCards.UpdateServiceCards;
using MasrafProject.Application.Features.Users.CreateUsers;
using MasrafProject.Application.Features.Users.DeleteUsers;
using MasrafProject.Application.Features.Users.GetAllUsers;
using MasrafProject.Application.Features.Users.GetByIdUsers;
using MasrafProject.Application.Features.Users.UpdateUsers;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers;
[AllowAnonymous]
public sealed class ServiceCardsController : ApiController
{
    public ServiceCardsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateServiceCard(CreateServiceCardCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Ekleme işlemi başarılı." })
        : BadRequest(new { message = "Ekleme işlemi başarısız." });
    }


    [HttpPost]
    public async Task<IActionResult> ServiceCardGetById(GetServiceCardByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> ServiceCardDelete(DeleteServiceCardCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllServiceCard(GetAllServiceCardQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateServiceCard(UpdateServiceCardCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Güncelleme işlemi başarılı." })
         : BadRequest(new { message = "Güncelleme işlemi başarısız." });
    }
}
