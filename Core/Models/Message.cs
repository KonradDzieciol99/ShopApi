﻿using Core.Entities;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Message
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(List<string> to, string subject, string content)
        {
            this.To = to;
            Subject = subject;
            Content = content;
        }

        //public List<MailboxAddress> To { get; set; }
        //public string Subject { get; set; }
        //public string Content { get; set; }
        //public Message(IEnumerable<string> to, string subject, string content)
        //{
        //    To = new List<MailboxAddress>();
        //    To.AddRange(to.Select(x => new MailboxAddress("email", x)));
        //    Subject = subject;
        //    Content = content;
        //}
    }
}
