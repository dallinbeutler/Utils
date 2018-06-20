using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Mailing
{
    public class SMTP
    {
        public List<String> Recipients { get; set; }
        public List<String> CCRecipients { get; set; }
        public List<String> BCCRecipients { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public string bodyMessage { get; set; }
        public string attachment { get; set; }
        public SMTP()
        {
            Recipients = new List<string>();
            CCRecipients = new List<string>();
            BCCRecipients = new List<string>();
            from = "EMS@LenderLive.com";       // Change to what you want
            subject = "";
            bodyMessage = "";
            attachment = "";
        }// end Contructor
        public void SendEmail(string to, string cc, string subject, string body, string attachment)
        {
            using (SmtpClient client = new SmtpClient(@"Outlook.corp.lcl"))
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress("NoReplyEMS@LenderLive.com");
                message.Subject = subject;
                message.Body = body;
                message.Attachments.Add(new Attachment(attachment));
                client.Send(message);
            }// end using 
        }// end SendEmail
        public void SendEmail()
        {
            using (SmtpClient client = new SmtpClient(@"Outlook.corp.lcl"))
            {
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress(this.from);
                if (Recipients.Count > 0)
                    foreach (string email in Recipients)
                    {
                        if (email != "")
                            message.To.Add(email);
                    }
                if (CCRecipients.Count > 0)
                    foreach (string email in CCRecipients)
                    {
                        if (email != "")
                            message.CC.Add(email);
                    }
                if (BCCRecipients.Count > 0)
                    foreach (string email in BCCRecipients)
                        message.Bcc.Add(email);
                message.Subject = subject;
                message.Body = bodyMessage;
                if (attachment.Length > 3)
                    message.Attachments.Add(new Attachment(attachment));
                client.Send(message);
            }// end using 
        }// end SendEmail
        private AlternateView getEmbeddedImage(String filePath)
        {
            LinkedResource res = new LinkedResource(filePath);
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, System.Net.Mime.MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }
    }
}
