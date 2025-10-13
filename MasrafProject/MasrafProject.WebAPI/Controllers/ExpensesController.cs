using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace MasrafProject.WebAPI.Controllers;
[AllowAnonymous]
public class ExpensesController :ApiController
{
    public ExpensesController(IMediator mediator) : base(mediator)
    {
    }
}
