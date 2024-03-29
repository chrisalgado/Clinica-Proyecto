﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;


namespace DataAccess.MailServices
{
    public abstract class MasterMailServer
    {
        private SmtpClient smtpClient;
        protected string senderMail { get; set; }
        protected string password { get; set; }
        protected string host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }

        protected void initializeSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(senderMail, password);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
        }

        //metodo publico para enviar mensajes de correo
        public void sendMail(string subject, string body, List<string> recipientMail) //de tipo lista ya que no manda mensaje por mensaje, sino que a todos al mismo tiempo

        {
            var mailMessage = new MailMessage();
            try
            {
                mailMessage.From = new MailAddress(senderMail);//DE DONDE LO MANDAMOS
                //foreach para que la lista agrege los correos 
                foreach (string mail in recipientMail)
                {
                    mailMessage.To.Add(mail); //PARA QUIEN SERÁ EL MENSAJE
                }
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex){ }
            finally
            {
                mailMessage.Dispose();
                smtpClient.Dispose(); 
            }
        }

    }
}
