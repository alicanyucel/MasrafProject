using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;
using MasrafProject.Application.Interfaces;

namespace MasrafProject.Application.Features.Companies.UpdateCompany;

internal sealed class UpdateCompanyCommandHandler(
    ICompanyRepository companyRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ITenantProvider tenantProvider) : IRequestHandler<UpdateCompanyCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        Company? company = await companyRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (company is null)
        {
            return Result<string>.Failure("Þirket bulunamadý.");
        }
        mapper.Map(request, company);
        company.TenantId = tenantProvider.TenantId;
        companyRepository.Update(company);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Þirket güncellendi.");
    }
}
