using Core.Baskets.Commands;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Baskets.Handlers
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBasketCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basket = await _unitOfWork.CustomerBasketRepository.FindOneAsync(x => x.AppUserId == request.UserId);
            if (basket == null) { throw new BadRequestException("Basket no longer exists"); }

            _unitOfWork.CustomerBasketRepository.Remove(basket);
            if (await _unitOfWork.Complete()) { return Unit.Value; }
            throw new BadRequestException("Failed to remove basket");
        }
    }
}
