using AutoMapper;
using Core.Brands.Queries;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Brands.Handlers
{
    internal class GetBrandOfProductsQueryHandler : IRequestHandler<GetBrandOfProductsQuery, IEnumerable<BrandOfProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBrandOfProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandOfProductDto>> Handle(GetBrandOfProductsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<BrandOfProductDto>>(await _unitOfWork.BrandOfProductRepository.GetAllAsync());
        }
    }
}
