using AutoMapper;
using Core.Categories.Commands;
using Core.Categories.Queries;
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

namespace Core.Categories.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryOfProductDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryOfProductDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new CategoryOfProduct() { CategoryName = request.Name };
            _unitOfWork.CategoryOfProductRepository.Add(category);
            if (await _unitOfWork.Complete()) { return _mapper.Map<CategoryOfProductDto>(category); }
            throw new BadRequestException("Failed to create category");
        }
    }
}
