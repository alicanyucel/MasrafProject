using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.GetAllCompany;

public sealed record GetAllCompaniesQuery() : IRequest<Result<List<Company>>>;
