using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using Base.BaseUtils;

namespace Base.Emails
{
    public class SendMail
    {
        public static bool SendSimpleMail(EMailParams mailParams, MailServerParams mailServerParams)
        {
            bool result = true;

            if (mailParams.WithSmtp)
            {
                try
                {
                    var msg = new EnhancedMailMessage();
                    if (!string.IsNullOrEmpty(mailParams.From))
                        msg.From = new MailAddress(mailParams.From);
                    msg.FromName = mailParams.FromName;
                    msg.To.Add(mailParams.To.Aggregate((a, b) => a + "," + b));
                    msg.Subject = mailParams.Subject;
                    msg.Body = mailParams.Body;
                    if (mailParams.CC != null && mailParams.CC.Count > 0)
                        msg.CC.Add(mailParams.CC.Aggregate((a, b) => a + "," + b));
                    if (mailParams.BCC != null && mailParams.BCC.Count > 0)
                        msg.Bcc.Add(mailParams.BCC.Aggregate((a, b) => a + "," + b));
                    msg.UseDefaultCredentials = mailParams.UseDefaultCredentials;
                    msg.SMTPServerName = mailServerParams.Host;
                    if (!string.IsNullOrEmpty(mailServerParams.User))
                        msg.SMTPUserName = mailServerParams.User;
                    if (!string.IsNullOrEmpty(mailServerParams.Pass))
                        msg.SMTPUserPassword = mailServerParams.Pass;
                    if (UtilsGeneral.ToInteger(mailServerParams.Port, 0) != 0)
                        msg.SMTPServerPort = mailServerParams.Port;
                    msg.SMTPSSL = mailParams.SMTPSSL;
                    if (!String.IsNullOrEmpty(mailParams.SmtpDomain))
                        msg.SmtpDomain = mailParams.SmtpDomain;

                    if (mailParams.AttachFiles != null && mailParams.AttachFiles.GetLength(0) > 0)
                    {
                        for (int i = 0; i < mailParams.AttachFiles.GetLength(0); i++)
                        {
                            msg.Attachments.Add(mailParams.AttachFiles[i]);
                        }
                    }

                    msg.IsBodyHtml = true;

                    msg.Send();
                }
                catch
                {
                    result = false;
                }
            }

            else
            {

                try
                {

                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(mailParams.From);
                    mailMessage.To.Add(mailParams.To.Aggregate((a, b) => a + "," + b));
                    mailMessage.Subject = mailParams.Subject;
                    mailMessage.Body = mailParams.Body;
                    if (mailParams.CC != null && mailParams.CC.Count > 0)
                        mailMessage.CC.Add(mailParams.CC.Aggregate((a, b) => a + "," + b));

                    if (mailParams.BCC != null && mailParams.BCC.Count > 0)
                        mailMessage.Bcc.Add(mailParams.BCC.Aggregate((a, b) => a + "," + b));

                    var smtp = new SmtpClient
                    {
                        Host = mailServerParams.Host,
                        Port = mailServerParams.Port
                    };

                    if (mailParams.AttachFiles != null && mailParams.AttachFiles.GetLength(0) > 0)
                    {
                        for (int i = 0; i < mailParams.AttachFiles.GetLength(0); i++)
                        {
                            mailMessage.Attachments.Add(mailParams.AttachFiles[i]);
                        }
                    }

                    mailMessage.IsBodyHtml = mailParams.IsHtml;

                    smtp.Credentials = new System.Net.NetworkCredential(mailServerParams.User, mailServerParams.Pass,
                                                                        mailServerParams.Domain);
                    smtp.Send(mailMessage);
                }
                catch
                {
                    result = false;
                }
            }

            return result;
        }
    }

    public class EnhancedMailMessage : MailMessage
    {
        private string _fromName;
        private string _smtpServerName;
        private string _smtpUserName;
        private string _smtpUserPassword;
        private int _smtpServerPort;
        private bool _smtpSsl;
        private bool _useDefaultCredentials;
        private string _smtpDomain;
        private bool _pickupDirectoryFromIis;

        public EnhancedMailMessage()
        {
            _fromName = string.Empty;
            _smtpServerName = string.Empty;
            _smtpUserName = string.Empty;
            _smtpUserPassword = string.Empty;
            _smtpServerPort = 25;
            _smtpSsl = false;
        }

        public string FromName
        {
            set
            {
                _fromName = value;
            }
            get
            {
                return _fromName;
            }
        }

        public string SMTPServerName
        {
            set
            {
                _smtpServerName = value;
            }
            get
            {
                return _smtpServerName;
            }
        }

        public string SMTPUserName
        {
            set
            {
                _smtpUserName = value;
            }
            get
            {
                return _smtpUserName;
            }
        }

        public string SMTPUserPassword
        {
            set
            {
                _smtpUserPassword = value;
            }
            get
            {
                return _smtpUserPassword;
            }
        }

        public int SMTPServerPort
        {
            set
            {
                _smtpServerPort = value;
            }
            get
            {
                return _smtpServerPort;
            }
        }

        public bool SMTPSSL
        {
            set
            {
                _smtpSsl = value;
            }
            get
            {
                return _smtpSsl;
            }
        }

        public bool UseDefaultCredentials
        {
            get { return _useDefaultCredentials; }
            set { _useDefaultCredentials = value; }
        }

        public string SmtpDomain
        {
            get { return _smtpDomain; }
            set { _smtpDomain = value; }
        }

        public bool PickupDirectoryFromIis
        {
            get { return _pickupDirectoryFromIis; }
            set { _pickupDirectoryFromIis = value; }
        }

        public void Send()
        {
            try
            {
                if (_smtpServerName.Length == 0)
                {
                    throw new Exception("SMTP Server not specified");
                }

                if (_fromName.Length > 0)
                {
                    Headers.Add("From", string.Format("{0} <{1}>", FromName, From));
                }

                var smtpClient = new SmtpClient();
                smtpClient.EnableSsl = _smtpSsl;
                smtpClient.Host = _smtpServerName;
                smtpClient.Port = _smtpServerPort;
                smtpClient.UseDefaultCredentials = _useDefaultCredentials;
                smtpClient.Credentials = new NetworkCredential(_smtpUserName, _smtpUserPassword, _smtpDomain);
                if (_pickupDirectoryFromIis)
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                smtpClient.Send(this);
            }
            catch { }
        }
    }

    public class EMailParams
    {
        public string From { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Attachment[] AttachFiles { get; set; }
        public bool IsHtml { get; set; }
        public bool SMTPSSL { get; set; }
        public string FromName { get; set; }
        public bool WithSmtp { get; set; }
        public bool UseSmtpSsl { get; set; }
        public string SmtpDomain { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool PickupDirectoryFromIis { get; set; }
    }

    public class MailServerParams
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Domain { get; set; }
    }
}
