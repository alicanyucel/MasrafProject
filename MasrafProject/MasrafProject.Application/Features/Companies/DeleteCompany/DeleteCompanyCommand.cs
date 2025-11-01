using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.DeleteCompany;

public sealed record DeleteCompanyCommand(int CompanyId) : IRequest<Result<string>>;
