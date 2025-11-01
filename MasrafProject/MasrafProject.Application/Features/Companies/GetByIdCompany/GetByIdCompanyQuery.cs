using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.GetByIdCompany;

public sealed record GetCompanyByIdQuery(int CompanyId) : IRequest<Result<Company>>;
