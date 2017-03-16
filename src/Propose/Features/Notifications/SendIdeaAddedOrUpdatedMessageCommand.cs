using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
            public SendIdeaAddedOrUpdatedMessageHandler(ProposeContext context, ICache cache, ISmtpConfiguration smtpConfiguration)
            {
                _context = context;
                _cache = cache;

                _smtpClient = new System.Net.Mail.SmtpClient(smtpConfiguration.Host, smtpConfiguration.Port)
                {
                    Credentials = new NetworkCredential(smtpConfiguration.Username, smtpConfiguration.Password),
                    EnableSsl = true
                };
            }

            public async Task<SendIdeaAddedOrUpdatedMessageResponse> Handle(SendIdeaAddedOrUpdatedMessageRequest request)
            {
                var mailMessage = BuildMessage();
                ResolveRecipients(ref mailMessage);
                this._smtpClient.Send(mailMessage);
                return new SendIdeaAddedOrUpdatedMessageResponse();
            }

            public MailMessage BuildMessage()
            {
                var mailMessage = new MailMessage();
                var html = @"<html><body><h1>Test</h1></body></html>";
                mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
                return mailMessage;
            }

            public MailMessage ResolveRecipients(ref System.Net.Mail.MailMessage mailMessage)
            {
                mailMessage.To.Add("quinntynebrown@gmail.com");
                mailMessage.From = new MailAddress("quinntynebrown@gmail.com");
                return mailMessage;
            }
            
            private readonly System.Net.Mail.SmtpClient _smtpClient;
            private readonly ProposeContext _context;
            private readonly ICache _cache;
            private readonly ISmtpConfiguration _smptpConfiguration;
        }
    }
}