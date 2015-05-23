using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Mail
{
    public class SendGridModel
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public SendGridModel(string to, string from, string subject, string body)
        {
            this.To = to;
            this.From = from;
            this.Subject = subject;
            this.Body = body;
        }
    }
}
