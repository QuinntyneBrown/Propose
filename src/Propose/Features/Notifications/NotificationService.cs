using MediatR;
using Propose.Data;
using Propose.Features.Core;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Propose.Features.Notifications
{
    public interface INotificationService
    {
        MailMessage BuildMessage();
        MailMessage ResolveRecipients(ref System.Net.Mail.MailMessage mailMessage);

    }

    public class NotificationService: INotificationService
    {
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
