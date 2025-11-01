using AutoMapper;
using GenericRepository;
using MasrafProject.Domain.Entities;
using MasrafProject.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace MasrafProject.Application.Features.Companies.CreateCompany;

internal sealed class CreateCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCompanyCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        bool exists = await companyRepository
            .GetAll()
            .AnyAsync(x => !x.IsDeleted && x.Name == request.Name, cancellationToken);
        if (exists)
        {
            return Result<string>.Failure("Kayıt zaten ekli.");
        }

        Company company = mapper.Map<Company>(request);
        await companyRepository.AddAsync(company, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        company.TenantId = company.Id;
        companyRepository.Update(company);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Şirket kaydı yapıldı");
    }
}
