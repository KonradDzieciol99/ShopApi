using AutoMapper;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Payments.Commands;
using Core.Payments.Events;
using Core.Payments.EventsHandlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Payments.Handlers
{
    public class RejectPaymentWebhookCommandHandler : IRequestHandler<RejectPaymentWebhookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;

        public RejectPaymentWebhookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config,IMediator mediator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _config = config;
            this._mediator = mediator;
        }

        public async Task<Unit> Handle(RejectPaymentWebhookCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.FindOneAsync(x => x.PaymentIntentId == request.intentId);

            if (order == null) { throw new NotImplementedException(); };

            order.Status = OrderStatus.PaymentFailed;

            await _unitOfWork.Complete();

            if (await _unitOfWork.Complete()) {
                await _mediator.Publish(new PaymentRejectedEvent(order));
                return Unit.Value;
            }
            throw new BadRequestException("nie udało się odrzucić płatności");//or internal??
        }
    }
}
