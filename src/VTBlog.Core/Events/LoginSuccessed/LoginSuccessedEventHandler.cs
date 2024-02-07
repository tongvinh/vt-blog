using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace VTBlog.Core.Events.LoginSuccessed
{
    internal class LoginSuccessedEventHandler : INotificationHandler<LoginSuccessedEvent>
    {
        public Task Handle(LoginSuccessedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
