using MasrafProject.Application.Features.Expenses.CreateExpense;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers;
[AllowAnonymous]
public class ExpensesController :ApiController
{
    public ExpensesController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateExpense(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Ekleme işlemi başarılı." })
        : BadRequest(new { message = "Ekleme işlemi başarısız." });
    }
}
