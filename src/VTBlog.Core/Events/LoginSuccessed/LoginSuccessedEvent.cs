using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VTBlog.Core.Events.LoginSuccessed
{
    public class LoginSuccessedEvent(string email):INotification
    {
        public string Email { get; set; } = email;
    }
}
