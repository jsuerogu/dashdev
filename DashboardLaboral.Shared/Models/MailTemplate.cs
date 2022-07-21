using System.Collections.Generic;
using System.Net.Mail;

namespace DashboarLaboral.Models
{
    public class MailTemplate
    {
        public List<MailMessage> MailMessages { get; set; } = new();
        public MailAddress FromAddress { get; internal set; }
    }
}
