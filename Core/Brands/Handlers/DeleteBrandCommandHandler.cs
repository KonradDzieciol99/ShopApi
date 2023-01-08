using AutoMapper;
using Core.Brands.Commands;
using Core.Dtos;
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
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandOfProductRepository.FindOneAsync(x => x.Id == request.Id);
            if (brand == null) { throw new NotImplementedException(); }
            _unitOfWork.BrandOfProductRepository.Remove(brand);

            if (await _unitOfWork.Complete()) { return Unit.Value; }

            throw new BadRequestException("Nie udało się usunąc brandu"); 
        }
    }
}
