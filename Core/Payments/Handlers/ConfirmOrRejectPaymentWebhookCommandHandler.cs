using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Payments.Commands;
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
    public class ConfirmOrRejectPaymentWebhookCommandHandler : IRequestHandler<ConfirmOrRejectPaymentWebhookCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly string _whSecret;
        public ConfirmOrRejectPaymentWebhookCommandHandler(IMediator mediator,IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            this._mediator = mediator;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _config = config;
            _whSecret = config.GetSection("StripeSettings:WhSecret").Value;//weebhook 

        }

        public async Task<Unit> Handle(ConfirmOrRejectPaymentWebhookCommand request, CancellationToken cancellationToken)
        {
            var stripeEvent = EventUtility.ConstructEvent(request.Json, request.StripeSignature, _whSecret);//throw error if not match 

            PaymentIntent intent;

            if (stripeEvent.Type == "payment_intent.payment_failed")
            {
                intent = (PaymentIntent)stripeEvent.Data.Object;
                await _mediator.Send(new RejectPaymentWebhookCommand(intent.Id));
                //_mediator_publish
            }
            else if (stripeEvent.Type == "payment_intent.succeeded")
            {
                intent = (PaymentIntent)stripeEvent.Data.Object;
                await _mediator.Send(new ConfirmPaymentWebhookCommand(intent.Id));
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                throw new BadRequestException("Unhandled event type");
            }
           
            return Unit.Value;
        }
    }
}
