using AutoMapper;
using Core.Categories.Queries;
using Core.Dtos;
using Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Categories.Handlers
{
    internal class GetCategoryOfProductsQueryHandler : IRequestHandler<GetCategoryOfProductsQuery, IEnumerable<CategoryOfProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryOfProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryOfProductDto>> Handle(GetCategoryOfProductsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<CategoryOfProductDto>>(await _unitOfWork.CategoryOfProductRepository.GetAllAsync());
        }
    }
}
