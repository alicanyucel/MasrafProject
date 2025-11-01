using MasrafProject.Application.Features.Companies.CreateCompany;
using MasrafProject.Application.Features.Companies.DeleteCompany;
using MasrafProject.Application.Features.Companies.GetAllCompany;
using MasrafProject.Application.Features.Companies.GetByIdCompany;
using MasrafProject.Application.Features.Companies.UpdateCompany;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers
{
    [AllowAnonymous]
    public class CompaniesController : ApiController
    {
        public CompaniesController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return response.IsSuccessful ? Ok(new { message = "Ekleme işlemi başarılı." })
            : BadRequest(new { message = "Ekleme işlemi başarısız." });
        }

        [HttpPost]
        public async Task<IActionResult> CompanyGetById(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCompany(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);

            if (result.IsSuccessful)
                return Ok(new { message = result.Data });

            var error = result.ErrorMessages is { Count: > 0 }
                ? string.Join(" | ", result.ErrorMessages)
                : "Şirket silinemedi.";
            return BadRequest(new { message = error });
        }

        [HttpPost]
        public async Task<IActionResult> GetAllCompanies(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCompany(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return response.IsSuccessful ? Ok(new { message = "Güncelleme işlemi başarılı." })
            : BadRequest(new { message = "Güncelleme işlemi başarısız." });
        }
    }
}
