using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.GetByIdCompany;

public sealed class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Result<Company>>
{
    private readonly ICompanyRepository _companyRepository;

    public GetCompanyByIdQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Result<Company>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByExpressionAsync(
            x => x.Id == request.CompanyId && !x.IsDeleted,
            cancellationToken);

        if (company is null)
            return Result<Company>.Failure($"Şirket bulunamadı: {request.CompanyId}");

        return Result<Company>.Succeed(company);
    }
}
