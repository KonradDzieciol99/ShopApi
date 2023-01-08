using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models;
using FluentEmail.Core;
using FluentEmail.Razor;
using Infrastructure.Common.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    //public class EmailService : IEmailService
    //{
    //    private readonly IFluentEmail _fluentEmail;
    //    private readonly IWebHostEnvironment _environment;
    //    private readonly string _from;
    //    public EmailService(IFluentEmail fluentEmail, IConfiguration configuration, IWebHostEnvironment environment)//
    //    {
    //        this._fluentEmail = fluentEmail;
    //        this._environment = environment;
    //        this._from = configuration["EmailConfiguration:From"];
    //        //_emailConfig = new EmailConfiguration
    //        //{
    //        //    From = configuration["EmailConfiguration:From"],
    //        //    SmtpServer = configuration["EmailConfiguration:SmtpServer"],
    //        //    Port = int.Parse(configuration["EmailConfiguration:Port"]),
    //        //    UserName = configuration["EmailConfiguration:Username"],
    //        //    Password = configuration["EmailConfiguration:Password"]
    //        //};

    //    }

    //    //private readonly EmailConfiguration _emailConfig;
    //    //public void SendWelcomeEmail(Message message)
    //    //{
    //    //    //string template = System.IO.File.ReadAllText(@"..\Core\Common\EmailTemplates\Welcome.cshtml");
    //    //    //var emailMessage = CreateEmailMessage(message, template);
    //    //    //Send(emailMessage);
    //    //}
    //    //public void SendConfirmPaymentEmail(Message message, Order order)
    //    //{
    //    //    //string template = System.IO.File.ReadAllText(@"..\Core\Common\EmailTemplates\PaymendConfirmed.cshtml");
    //    //    //var emailMessage = CreateEmailMessage(message,template);
    //    //    //Send(emailMessage);
    //    //}

    //    //public void SendPendingOrderEmail(Message message, Order order)
    //    //{
    //    //    //string template = System.IO.File.ReadAllText(@"..\Core\Common\EmailTemplates\OrderPending.cshtml");
    //    //    //var emailMessage = CreateEmailMessage(message, template);
    //    //    //Send(emailMessage);
    //    //}

    //    //public void SendRejectPaymentEmail(Message message, Order order)
    //    //{
    //    //    //string template = System.IO.File.ReadAllText(@"..\Core\Common\EmailTemplates\PaymentRejected.cshtml");
    //    //    //var emailMessage = CreateEmailMessage(message, template);
    //    //    //Send(emailMessage);
    //    //}
    //    //private MimeMessage CreateEmailMessage(Message message, string template,bool templateActive = true)
    //    //{

    //    //    //string result;
    //    //    //if (templateActive)
    //    //    //{
    //    //    //    var templateKey = template;//czyli po prostu jakaś nazwa
    //    //    //    if (Engine.Razor.IsTemplateCached(templateKey, null))
    //    //    //    {
    //    //    //        result = Engine.Razor.Run(templateKey, null, message);//w miejscu null object dla razorPage
    //    //    //    }
    //    //    //    else
    //    //    //    {
    //    //    //        result = Engine.Razor.RunCompile(template, templateKey, null, message);//w miejscu null object dla razorPage
    //    //    //    }
    //    //    //}
    //    //    //else
    //    //    //{
    //    //    //    result = template;
    //    //    //}

    //    //    //var emailMessage = new MimeMessage();
    //    //    //emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
    //    //    //emailMessage.To.AddRange(message.To);
    //    //    //emailMessage.Subject = message.Subject;
    //    //    //var bodyBuilder = new BodyBuilder();
    //    //    //bodyBuilder.HtmlBody = result;
    //    //    //bodyBuilder.TextBody = "Placeholder";
    //    //    //emailMessage.Body = bodyBuilder.ToMessageBody();

    //    //    //return emailMessage;
    //    //}
    //    public async void SendContactMessage(Message message)//task
    //    {
    //        //DirectoryInfo directoryInfo = new DirectoryInfo(_environment.ContentRootPath);
    //        //DirectoryInfo directoryInfo2 = new DirectoryInfo(@"..\");
    //        //var fdf = directoryInfo.Parent.GetFiles();
    //        //var dfsd = _environment.ContentRootPath;
    //        //var dfsdddd = _environment.WebRootFileProvider;
    //        ////var tes447 = File.Exists(@"\Controllers");
    //        //var tes447dd = Directory.Exists(@"..\Core\Common\EmailTemplates\PaymentRejected.cshtml");
    //        //var tes447ddee = File.Exists(@"..\Core\Common\EmailTemplates\PaymentRejected.cshtml");
    //        //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Names.txt");
    //        //string sdfsdf = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
    //        //var fdsdfg = AppDomain.CurrentDomain.BaseDirectory;
    //        //var fdsdfg2 = AppDomain.CurrentDomain.RelativeSearchPath;
    //        //var test_0 = Directory.GetParent(System.Reflection.Assembly.GetEntryAssembly().Location);
    //        ////var test0 = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

    //        ////File.ReadAllText(Path.Combine(templatePath, viewName));
    //        //var tes2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates.json");
    //        //var test4 = AppDomain.CurrentDomain.BaseDirectory;
    //        //var test5 = Path.Combine(Directory.GetCurrentDirectory(), @"..\Data\Names.txt");
    //        //string sdf = Directory.GetCurrentDirectory();

    //        //var test55 = Directory.GetCurrentDirectory();
    //        ////Environment.get
    //        //var tes6 = Assembly.GetExecutingAssembly().Location;
    //        //var tes7 = Directory.Exists(@"C:\Users\konra\Source\Repos\ShopWebApiNew\Infrastructure\Common\EmailTemplates");
    //        //// string[] files = Directory.GetFiles(@".\EmailTemplates");

    //        ////C:\Users\konra\Source\Repos\ShopWebApiNew\Infrastructure\Common\EmailTemplates
    //        ////$"{Directory.GetCurrentDirectory()}/Common/EmailTemplates/Mytemplate.cshtml"
    //        //var template = @"Dear @Model.Name, You are totally @Model.Compliment.";
    //        ////\Common\EmailTemplates\ContactEmail.cshtml;
    //        //var model = new { Name = "Luke", Compliment = "Awesome" };
    //        //var modelsx = "konrad";

    //        var ggggg = new TestEmailModel() { Name = "dd" };
    //        var ddd = AppDomain.CurrentDomain.BaseDirectory;
    //        string exe_path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
    //        //string path2 = $@"{exe_path}\Common\EmailTemplates\Emails\ContactEmail.cshtml";
    //        //if (!File.Exists(path2))
    //        //{
    //        //    throw new FileNotFoundException($"Path{path2}");
    //        //}

    //        //Common\EmailTemplates\Views\ConfirmAccountEmail.cshtml
    //        //Common\EmailTemplates\Emails\ContactEmail.cshtml
    //        //Common\EmailTemplates\Shared2\EmailLayout.cshtml
    //        //
    //        var sdfsd = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
    //        string[] files = Assembly.GetExecutingAssembly().GetManifestResourceNames();

    //        var fsd = this.GetType().Assembly;
    //        var email = _fluentEmail
    //                   .To("konradd990212@gmail.com")
    //                   .Tag("TEST")
    //                   .Subject("Razor template example")
    //                   //.UsingTemplateFromFile($@"{exe_path}\Common\EmailTemplates\Emails\ContactEmail.cshtml", ggggg);//Assembly.Load("Common.EmailTemplates.Emails.ContactEmail.cshtml")
    //                   //
    //                   //"Infrastructure.Common.EmailTemplates.Emails.ContactEmail.cshtml"
    //                   //Infrastructure.
    //                   //"Infrastructure.Common.EmailTemplates.Shared._Layout.cshtml"
    //                   .UsingTemplateFromEmbedded("Infrastructure.Common.EmailTemplates.Emails.ContactEmail.cshtml", ggggg, Assembly.GetExecutingAssembly());
    //        //Assembly.GetExecutingAssembly()
    //        // GetType().Assembly 
    //        //        ddddddddddddddddddddddddddd email.To(configuration["emailTo"])
    //        // .SetFrom(configuration["emailFrom"])
    //        // .Subject(configuration["emailSubject"])
    //        // .UsingTemplateFromEmbedded(
    //        //  "YourCompany.YourRazorClassLibrary.EmailTemplate.cshtml",
    //        //new { Name = "Leniel Maccaferri", Compliment = "Cool" },
    //        //Assembly.Load("YourCompany.YourRazorClassLibrary"));


    //        //var email = _fluentEmail.To(_from)
    //        //                       .Subject("Razor template example")
    //        //                       .UsingTemplateFromFile(@"C:\Users\konra\Source\Repos\ShopWebApiNew\Infrastructure\Common\EmailTemplates\ContactEmail.cshtml", ggggg);

    //        var resoult = await email.SendAsync();

    //        if (!resoult.Successful)
    //        {
    //            throw new BadRequestException("nie udało się wysłać wiadomości");
    //        }

    //        //Email.DefaultRenderer = new RazorRenderer();

    //        //var template = "Dear @Model.Name, You are totally @Model.Compliment.";

    //        //var kk = Email
    //        //    .From(_from)
    //        //    .To(_from, _from)
    //        //    .Subject("woo nuget")
    //        //    .UsingTemplate(template, new { Name = "Luke", Compliment = "Awesome" });
    //        //var resoult = await kk.SendAsync();

    //        //await _fluentEmail.To(_from)
    //        //                .Subject("Razor template example")
    //        //                .UsingTemplate(template, model)
    //        //                .SendAsync();

    //        //await email.SendAsync();

    //        //string template = message.To[0].Address +" <br> "+message.Content;
    //        //var selfEmail = new MailboxAddress("email", _emailConfig.From);
    //        //message.To = new List<MailboxAddress>() { selfEmail };
    //        //var emailMessage = CreateEmailMessage(message , template,  false);
    //        //Send(emailMessage);
    //    }
    //    //private void Send(MimeMessage mailMessage)
    //    //{
    //    //    //using (var client = new SmtpClient())
    //    //    //{
    //    //    //    try
    //    //    //    {
    //    //    //        client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
    //    //    //        client.AuthenticationMechanisms.Remove("XOAUTH2");
    //    //    //        client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
    //    //    //        client.Send(mailMessage);
    //    //    //    }
    //    //    //    catch
    //    //    //    {
    //    //    //        //log an error message or throw an exception or both.
    //    //    //        throw;
    //    //    //    }
    //    //    //    finally
    //    //    //    {
    //    //    //        client.Disconnect(true);
    //    //    //        client.Dispose();
    //    //    //    }
    //    //    //}
    //    //}

    //    public static void FileName()
    //    {
    //        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates.json");
    //    }
    //}
}
