using Core.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contact.Commands
{
    public  class SendContactMessageCommand:IRequest<Unit>
    {
        public SendContactMessageCommand(ContactMessageDto contactMessageDto)
        {
            ContactMessageDto = contactMessageDto;
        }

        public ContactMessageDto ContactMessageDto { get; }
    }
}
