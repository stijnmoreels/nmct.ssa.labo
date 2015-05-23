using System;
namespace nmct.ssa.labo.webshop.businesslayer.Mail
{
    public interface ISendGridHelper
    {
        void SendMail(SendGridModel model);
    }
}
