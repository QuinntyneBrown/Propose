using System;
using System.Configuration;

namespace Propose.Features.Notifications
{
    public interface INotificationsConfiguration
    {
        string Host { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string SendGridApiKey { get; set; }
    }

    public class NotificationsConfiguration : ConfigurationSection, INotificationsConfiguration
    {
        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return (string)this["host"]; }
            set { this["host"] = value; }
        }

        [ConfigurationProperty("sendGridApiKey", IsRequired = true)]
        public string SendGridApiKey
        {
            get { return (string)this["sendGridApiKey"]; }
            set { this["sendGridApiKey"] = value; }
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public int Port
        {
            get { return (int)this["port"]; }
            set { this["port"] = value; }
        }

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username
        {
            get { return (string)this["username"]; }
            set { this["username"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
            set { this["password"] = value; }
        }
        

        public static readonly Lazy<INotificationsConfiguration> LazyConfig = new Lazy<INotificationsConfiguration>(() =>
        {
            var section = ConfigurationManager.GetSection("notificationsConfiguration") as INotificationsConfiguration;
            if (section == null)
            {
                throw new ConfigurationErrorsException("notificationsConfiguration");
            }

            return section;
        }, true);
    }
}
