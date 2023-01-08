using AutoMapper;
using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Products.Commands;
using Core.Products.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Handlers
{
    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity=await _unitOfWork.ProductRepository.GetOneAsync(request.Id);
            if (entity == null)
            {
                throw new NotImplementedException();
            }

            _unitOfWork.ProductRepository.Remove(entity);

            if (await _unitOfWork.Complete()) { return Unit.Value; }
            throw new BadRequestException("nie udało się usunąć produktu");


        }
    }
}
