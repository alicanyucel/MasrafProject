using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.UpdateCompany;

public sealed record UpdateCompanyCommand(
int Id,
int TenantId,
string Name,
string Email,
string PhoneNumber,
string Address
) : IRequest<Result<string>>;
