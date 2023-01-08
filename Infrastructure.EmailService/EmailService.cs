using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using FluentEmail.Core;
using Infrastructure.EmailService.Views.Emails;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly string _from;
        public EmailService(IFluentEmail fluentEmail, IConfiguration configuration)
        {
            this._fluentEmail = fluentEmail;
            this._from = configuration["EmailConfiguration:From"];
        }

        public async Task SendConfirmPaymentEmail(Message message, Order order)
        {
            string exe_path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            var email = _fluentEmail
                       .To(message.To[0])
                       .Tag("TEST")
                       .Subject(message.Subject)
                       .UsingTemplateFromFile($@"{exe_path}/Views/Emails/PaymentSuccessEmail.cshtml", order);

            var resoult = await email.SendAsync();

            if (!resoult.Successful)
            {
                throw new BadRequestException("Can't send a Email");
            }
        }

        public async Task SendContactMessage(Message message)//task
        {
            var template = "Message from @Model.Email,<br>Message content:<br> @Model.Content.";
            var email = _fluentEmail
                       .To(_from)
                       .Tag("TEST")
                       .Subject(message.Subject)
                       .UsingTemplate(template, new { Email = message.To[0], Content = message.Content });

            var resoult = await email.SendAsync();

            if (!resoult.Successful)
            {
                throw new BadRequestException("Can't send a message");
            }
        }

        public async Task SendPendingOrderEmail(Message message, Order order)
        {
            string exe_path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            var email = _fluentEmail
                       .To(message.To[0])
                       .Tag("TEST")
                       .Subject(message.Subject)
                       .UsingTemplateFromFile($@"{exe_path}/Views/Emails/PendingOrderEmail.cshtml", order);

            var resoult = await email.SendAsync();

            if (!resoult.Successful)
            {
                throw new BadRequestException("Can't send a Email");
            }
        }

        public async Task SendWelcomeEmail(Message message)
        {
            var model = new WelcomeEmailModel() { Name = message.To[0] };
            string exe_path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            var template = "Message from @Model.Email,<br>Message content:<br> @Model.Content.";
            var email = _fluentEmail
                       .To(message.To[0])
                       .Tag("TEST")
                       .Subject(message.Subject)
                       .UsingTemplateFromFile($@"{exe_path}/Views/Emails/WelcomeEmail.cshtml", model);

            var resoult = await email.SendAsync();

            if (!resoult.Successful)
            {
                throw new BadRequestException("Can't send a message");
            }
        }
        //public async void SendContactMessage(Message message)//task
        //{

        //    var TestModel = new ContactEmailModel() { Name = "dd" };
        //    string exe_path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        //    var email = _fluentEmail
        //               .To("konradd990212@gmail.com")
        //               .Tag("TEST")
        //               .Subject("Razor template example")
        //               .UsingTemplateFromFile($@"{exe_path}/Views/Emails/WelcomeEmail.cshtml", TestModel);

        //    var resoult = await email.SendAsync();

        //    if (!resoult.Successful)
        //    {
        //        throw new BadRequestException("nie udało się wysłać wiadomości");
        //    }
        //}
    }
}