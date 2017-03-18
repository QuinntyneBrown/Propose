using MediatR;
using System.Threading.Tasks;

namespace Propose.Features.Notifications
{
    public class SendIdeaAddedOrUpdatedMessageCommand
    {
        public class SendIdeaAddedOrUpdatedMessageRequest : IRequest<SendIdeaAddedOrUpdatedMessageResponse>
        {
            public int? IdeaId { get; set; }
        }

        public class SendIdeaAddedOrUpdatedMessageResponse { }

        public class SendIdeaAddedOrUpdatedMessageHandler : IAsyncRequestHandler<SendIdeaAddedOrUpdatedMessageRequest, SendIdeaAddedOrUpdatedMessageResponse>
        {
            public SendIdeaAddedOrUpdatedMessageHandler(INotificationService notificationService)
            {
                _notificationService = notificationService;
            }

            public async Task<SendIdeaAddedOrUpdatedMessageResponse> Handle(SendIdeaAddedOrUpdatedMessageRequest request)
            {
                var mailMessage = _notificationService.BuildMessage();
                _notificationService.ResolveRecipients(ref mailMessage);
                await _notificationService.SendAsync(mailMessage);
                return new SendIdeaAddedOrUpdatedMessageResponse();
            }            
            
            private readonly INotificationService _notificationService;
        }
    }
}