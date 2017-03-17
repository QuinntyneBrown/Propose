using Propose.Data;
using Propose.Features.Core;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace Propose.Features.Notifications
{
    public interface INotificationService
    {
        SendGridMessage BuildMessage();
        void ResolveRecipients(ref SendGridMessage mailMessage);        
    }

    public class NotificationService: INotificationService
    {
        public NotificationService(Lazy<NotificationsConfiguration> lazyNotificationsConfiguration)
        {
            _sendGridClient = new SendGridClient(lazyNotificationsConfiguration.Value.SendGridApiKey);
        }

        public SendGridMessage BuildMessage()
        {
            var mailMessage = new SendGrid.Helpers.Mail.SendGridMessage();            
            var html = @"<html><body><h1>Test</h1></body></html>";
            mailMessage.HtmlContent = html;
            return mailMessage;
        }

        public void ResolveRecipients(ref SendGridMessage mailMessage)
        {
            mailMessage.AddTo(new EmailAddress("quinntynebrown@gmail.com"));            
        }

        public async Task<dynamic> SendAsync(SendGridMessage mailMessage)
        {
            return await _sendGridClient.SendEmailAsync(mailMessage);
        }

        private readonly ProposeContext _context;
        private readonly ICache _cache;
        private readonly SendGridClient _sendGridClient;
        private readonly INotificationsConfiguration _smtpConfiguration;
    }
}
