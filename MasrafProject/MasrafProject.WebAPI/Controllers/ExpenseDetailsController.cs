using MasrafProject.Application.Features.ExpenseDetails.DeleteExpenseDetails;
using MasrafProject.Application.Features.ExpenseDetails.GetAllExpenseDetails;
using MasrafProject.Application.Features.ExpenseDetails.GetByIdExpenseDetails;
using MasrafProject.Application.Features.Users.CreateUsers;
using MasrafProject.Application.Features.Users.DeleteUsers;
using MasrafProject.Application.Features.Users.GetAllUsers;
using MasrafProject.Application.Features.Users.GetByIdUsers;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers;
[AllowAnonymous]
public sealed class ExpenseDetailsController : ApiController
{
    public ExpenseDetailsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> ExpenseDetailGetById(GetByIdExpenseDetailQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);

    }
    [HttpPost]
    public async Task<IActionResult> ExpenseDetailDelete(DeleteExpenseDetailsCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllExpenseDetails(GetAllExpenseDetailsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}