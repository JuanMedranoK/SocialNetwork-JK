using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mail
{
    public class EmailSender
    {
        public void SendEmail(string mailUser, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("phpitladiplomado@gmail.com");
                mail.To.Add(mailUser);
                mail.Subject = subject;
                mail.Body = body;
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("phpitladiplomado@gmail.com", "#Querty123");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
