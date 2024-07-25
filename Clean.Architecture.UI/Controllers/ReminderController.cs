using Hangfire;
using Microsoft.AspNetCore.Mvc;
using RingoMedia.App.Validation;
using RingoMedia.DataAccess.ViewModels;
using RingoMedia.UI.Services;

namespace RingoMedia.UI.Controllers
{
    public class ReminderController : Controller
    {
        private readonly ReminderAppService _reminderService;
        private readonly SendReminderEmailJob sendReminderEmailJob;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly EmailService _emailService;
        private readonly IConfiguration Configuration;
        public ReminderController(ReminderAppService reminderService, SendReminderEmailJob sendReminderEmailJob , IBackgroundJobClient backgroundJobClient, EmailService emailService, IConfiguration configuration)
        {
            _reminderService = reminderService;
            this.sendReminderEmailJob = sendReminderEmailJob;
            _backgroundJobClient = backgroundJobClient;
            _emailService = emailService;
            Configuration = configuration;

        }

        public IActionResult Index()
        {
            var model = _reminderService.GetAll();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReminderViewModel reminder)
        {
            if (ModelState.IsValid)
            {
                await _reminderService.CreateAsync(reminder);
                var toEmail = Configuration["Params:ToEmail"];
                //_emailService.SendEmailAsync(toEmail, $"Reminder: {reminder.Title}");
              //  await sendReminderEmailJob.SendEmail(reminder);
               ScheduleEmailNotification(reminder); // Call the scheduling method here
                return RedirectToAction(nameof(Index));
            }
            return View(reminder);
        }

        private void ScheduleEmailNotification(ReminderViewModel reminder)
        {
            // Use Hangfire to schedule the background job
            //DateTime scheduledTimeUtc = reminder.DateTime;
            //DateTime scheduledTimeLocal = scheduledTimeUtc.ToLocalTime();
            //TimeSpan delay = scheduledTimeLocal - DateTime.Now;
            BackgroundJob.Schedule(() => sendReminderEmailJob.SendEmail(reminder), reminder.DateTime);
        }
    }
}
