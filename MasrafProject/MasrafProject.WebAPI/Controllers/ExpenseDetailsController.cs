using MasrafProject.WebAPI.Abstractions;
using MediatR;

namespace MasrafProject.WebAPI.Controllers;

public sealed class ExpenseDetailsController : ApiController
{
    public ExpenseDetailsController(IMediator mediator) : base(mediator)
    {
    }
}
