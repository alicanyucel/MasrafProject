using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.CreateCompany;


public sealed record CreateCompanyCommand(
int TenantId,
string Name,
string Email,
string PhoneNumber,
string Address
) : IRequest<Result<string>>;