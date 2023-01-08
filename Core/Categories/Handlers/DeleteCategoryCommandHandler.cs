using AutoMapper;
using Core.Categories.Commands;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Products.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Categories.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryOfProductRepository.FindOneAsync(x => x.Id == request.Id);
            if (category == null) { throw new NotImplementedException(); }
            _unitOfWork.CategoryOfProductRepository.Remove(category);
            if (await _unitOfWork.Complete()) { return Unit.Value; }
            throw new BadRequestException("Nie udało się usunąć categorii");

        }
    }
}
