using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.UpdateCompany;

internal sealed class UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateCompanyCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        Company? company = await companyRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if (company == null)
        {
            return Result<string>.Failure("Şirket bulunamadı.");
        }
        mapper.Map(request, company);
        company.TenantId = company.Id;
        companyRepository.Update(company);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Şirket güncellendi.";

    }
}
