using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Threading.Tasks;

namespace Propose.Features.Notifications
{
    public class SendRegistrationConfirmationCommand
    {
        public class SendRegistrationConfirmationRequest : IRequest<SendRegistrationConfirmationResponse> { }

        public class SendRegistrationConfirmationResponse { }

        public class SendRegistrationConfirmationHandler : IAsyncRequestHandler<SendRegistrationConfirmationRequest, SendRegistrationConfirmationResponse>
        {
            public SendRegistrationConfirmationHandler(ProposeContext context, ICache cache, INotificationService notificationService)
            {
                _context = context;
                _cache = cache;
                _notificationService = notificationService;
            }

            public async Task<SendRegistrationConfirmationResponse> Handle(SendRegistrationConfirmationRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly ProposeContext _context;
            private readonly ICache _cache;
            private readonly INotificationService _notificationService;
        }

    }

}
