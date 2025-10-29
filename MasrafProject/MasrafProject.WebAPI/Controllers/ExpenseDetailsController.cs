using MasrafProject.Application.Features.ExpenseDetails.CreateExpenseDetails;
using MasrafProject.Application.Features.ExpenseDetails.DeleteExpenseDetails;
using MasrafProject.Application.Features.ExpenseDetails.GetAllExpenseDetails;
using MasrafProject.Application.Features.ExpenseDetails.GetByIdExpenseDetails;
using MasrafProject.Application.Features.ExpenseDetails.UpdateExpenseDetails;
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

        if (result is null)
            return NotFound("İlgili masraf kaydı bulunamadı.");

        return Ok(new
        {
            Message = "Masraf kaydı başarıyla getirildi.",
            Data = result
        });
    }

    [HttpPost]
    public async Task<IActionResult> ExpenseDetailDelete(DeleteExpenseDetailsCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return Ok(new
        {
            Message = "Masraf kaydı başarıyla silindi."
        });
    }

    [HttpPost]
    public async Task<IActionResult> GetAllExpenseDetails(GetAllExpenseDetailsQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(new
        {
            Message = "Tüm masraf kayıtları başarıyla listelendi.",
            Data = response
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDetails(UpdateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (!response.IsSuccessful)
            return BadRequest(new { Message = response.ErrorMessages });

        return Ok(new
        {
            Message = response.Data,
            Status = "Güncelleme başarılı"
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpenseDetails(CreateExpenseDetailCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (!response.IsSuccessful)
            return BadRequest(new { Message = response.ErrorMessages });

        return Ok(new
        {
            Message = response.Data,
            Status = "Oluşturma başarılı"
        });
    }
}
