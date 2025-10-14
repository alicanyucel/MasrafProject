using MasrafProject.Application.Features.Expenses.CreateExpense;
using MasrafProject.Application.Features.Expenses.DeleteExpense;
using MasrafProject.Application.Features.Expenses.GetAllExpense;
using MasrafProject.Application.Features.Expenses.GetByIdExpense;
using MasrafProject.Application.Features.Expenses.UpdateExpense;
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
    [HttpPost]
    public async Task<IActionResult> ExpenseGetById(GetByIdExpenseQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> ExpenseDelete(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllExpense(GetAllExpenseQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateExpense(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Güncelleme işlemi başarılı." })
         : BadRequest(new { message = "Güncelleme işlemi başarısız." });
    }
}
