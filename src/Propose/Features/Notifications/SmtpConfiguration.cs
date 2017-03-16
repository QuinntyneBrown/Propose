using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propose.Features.Notifications
{
    public interface ISmtpConfiguration
    {
        string Host { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }

    public class SmtpConfiguration : ConfigurationSection, ISmtpConfiguration
    {
        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return (string)this["host"]; }
            set { this["host"] = value; }
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

        public static SmtpConfiguration Config
        {
            get { return ConfigurationManager.GetSection("smtpConfiguration") as SmtpConfiguration; }
        }
    }
}
