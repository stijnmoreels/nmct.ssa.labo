using SendGrid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ssa.labo.webshop.businesslayer.Mail
{
    public class SendGridHelper
    {
        public void SendMail(SendGridModel model)
        {
            var message = new SendGridMessage();
            message.From = new MailAddress(model.From);
            message.AddTo(model.To);
            message.Subject = model.Subject;
            message.Html = model.Body;

            var credentials = new NetworkCredential("stijnmoreels", "P@ssw0rd");
            var transport = new Web(credentials);
            transport.Deliver(message);
        }
    }
}
