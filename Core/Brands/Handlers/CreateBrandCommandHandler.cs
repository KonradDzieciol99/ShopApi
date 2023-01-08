using AutoMapper;
using Core.Brands.Commands;
using Core.Brands.Queries;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Brands.Handlers
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BrandOfProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BrandOfProductDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = new BrandOfProduct() { BrandName = request.Name };
            _unitOfWork.BrandOfProductRepository.Add(brand);
            if (await _unitOfWork.Complete()) { return _mapper.Map<BrandOfProductDto>(brand); }
            throw new BadRequestException("Nie udało się utworzyć nowego brandu");
        }
    }
}
