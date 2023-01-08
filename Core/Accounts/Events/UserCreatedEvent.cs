using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Accounts.Events
{
    public class UserCreatedEvent:INotification
    {
        public UserCreatedEvent(AppUser appUser)
        {
            AppUser = appUser;
        }

        public AppUser AppUser { get; }
    }
}
