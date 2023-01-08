using Core.Accounts.Events;
using Core.Interfaces;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.EventsHandlers
{
    internal class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IEmailService _emailService;

        public UserCreatedEventHandler(IEmailService emailService)
        {
            this._emailService = emailService;
        }
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var message = new Message(new List<string>() { notification.AppUser.Email }, "Welcome!", "Welcome Content.");
            await _emailService.SendWelcomeEmail(message);
            //return Task.CompletedTask;
        }
    }
}
