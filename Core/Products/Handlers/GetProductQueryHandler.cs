using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Products.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Handlers
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IUnitOfWork unitOfWork,IMapper mapper )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
         
        async Task<ProductDto> IRequestHandler<GetProductQuery, ProductDto>.Handle(GetProductQuery request, CancellationToken cancellationToken)
        { 
            return _mapper.Map<ProductDto>(await _unitOfWork.ProductRepository.GetProductWithPhotosAsync(request.Id));
            //return _mapper.Map<ProductDto>(await _unitOfWork.ProductRepository.GetOneAsync(request.Id));
        }
    }
}
