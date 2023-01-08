using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Products.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Products.Handlers
{
    public class UpdateProductCommandHandler:IRequestHandler<UpdateProductCommand,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.ProductRepository.Update(_mapper.Map<Product>(request.ProduktDto));
            if (await _unitOfWork.Complete()){ return Unit.Value; }
            throw new BadRequestException("Nie udało się zaktualizować Produktu");
        }
    }
}
