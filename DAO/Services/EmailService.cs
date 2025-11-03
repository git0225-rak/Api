using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;


namespace Simpolo_Endpoint.DAO.Services
{
    public class EmailService
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string smtpUsername;
        private readonly string smtpPassword;
        private readonly bool enableSsl;

        public EmailService(IConfiguration configuration)
        {
            //var smtpSettings = configuration.GetSection("SmtpSettings");
            //smtpServer = smtpSettings["SmtpServer"];
            //smtpPort = int.Parse(smtpSettings["SmtpPort"]);
            //smtpUsername = smtpSettings["SmtpUsername"];
            //smtpPassword = smtpSettings["SmtpPassword"];
            //enableSsl = bool.Parse(smtpSettings["EnableSsl"]);
        }
        
        public async Task SendEmailAsync(string ToEmailID, string subject, string body)
        {
            //try
            //{
                

            //    using (SmtpClient smtpClient = new SmtpClient(smtpServer))
            //    {
            //        smtpClient.Port = smtpPort;
            //        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            //        smtpClient.EnableSsl = enableSsl;
            //        smtpClient.Timeout = 10000;

            //        using (MailMessage mailMessage = new MailMessage())
            //        {
            //            mailMessage.From = new MailAddress(smtpUsername);
            //            mailMessage.Subject = subject;
            //            mailMessage.Body = body;

            //            // Split the ToEmailID string into individual email addresses
            //            string[] toEmailAddresses = ToEmailID.Split(';');

            //            // Add each non-empty email address to the To collection
            //            foreach (var emailAddress in toEmailAddresses)
            //            {
            //                if (!string.IsNullOrWhiteSpace(emailAddress))
            //                {
            //                    mailMessage.To.Add(emailAddress.Trim());
            //                }
            //            }

            //            await smtpClient.SendMailAsync(mailMessage);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine($"Error sending email: {ex.Message}");
            //    throw;
            //}
        }

        //public async Task SendEmailAsync(string subject, string body)
        //{
        //    try
        //    {
        //        using (SmtpClient smtpClient = new SmtpClient(smtpServer))
        //        {
        //            smtpClient.Port = smtpPort;
        //            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
        //            smtpClient.EnableSsl = enableSsl;
        //            smtpClient.Timeout = 10000;

        //            using (MailMessage mailMessage = new MailMessage())
        //            {
        //                // Set SmtpUsername as the "From" email address
        //                mailMessage.From = new MailAddress(smtpUsername);

        //                // Set other properties
        //                mailMessage.To.Add(ToEmailID);
        //                mailMessage.Subject = subject;
        //                mailMessage.Body = body;

        //                await smtpClient.SendMailAsync(mailMessage);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error sending email: {ex.Message}");
        //        throw;
        //    }
        //}



    }
}
