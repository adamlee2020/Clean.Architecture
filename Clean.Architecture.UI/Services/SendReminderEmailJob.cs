using RingoMedia.DataAccess.Entities;
using RingoMedia.DataAccess.ViewModels;

namespace RingoMedia.UI.Services
{
    public class SendReminderEmailJob
    {
        private readonly EmailService _emailService;
        private readonly IConfiguration Configuration;

        public SendReminderEmailJob(EmailService emailService , IConfiguration configuration)
        {
            _emailService = emailService;
            Configuration = configuration;
        }

        public void SendEmail(ReminderViewModel reminder)
        {
            var toEmail = Configuration["Params:ToEmail"];
            _emailService.SendEmailAsync(toEmail, $"Reminder: {reminder.Title}").ConfigureAwait(false);
        }

    }
}
