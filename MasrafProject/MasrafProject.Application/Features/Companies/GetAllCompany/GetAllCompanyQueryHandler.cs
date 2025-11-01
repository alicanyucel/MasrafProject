using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.GetAllCompany;

public sealed class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, Result<List<Company>>>
{
    private readonly ICompanyRepository _companyRepository;

    public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Result<List<Company>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository
            .GetAll()
            .Where(c => !c.IsDeleted)
            .ToListAsync(cancellationToken);

        return Result<List<Company>>.Succeed(companies);
    }
}
