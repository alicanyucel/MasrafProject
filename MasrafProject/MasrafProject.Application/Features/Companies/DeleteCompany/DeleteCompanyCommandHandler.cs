using GenericRepository;
using MasrafProject.Domain.Repositories;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.DeleteCompany;

public sealed class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Result<string>>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByExpressionAsync(
            x => x.Id == request.CompanyId && !x.IsDeleted,
            cancellationToken);

        if (company is null)
            return Result<string>.Failure($"Şirket bulunamadı: {request.CompanyId}");

        company.IsDeleted = true;
        _companyRepository.Update(company);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed($"Şirket başarıyla silindi: {company.Name}");
    }
}

