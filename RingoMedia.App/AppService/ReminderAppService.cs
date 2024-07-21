using RingoMedia.DataAccess.ViewModels;
using RingoMedia.Service.Services;
using System.ComponentModel.DataAnnotations;

namespace RingoMedia.App.Validation
{
    public class ReminderAppService
    {
        private readonly ReminderService Service;
        public ReminderAppService(ReminderService service)
        {
            Service = service;
        }
        public List<ReminderViewModel> GetAll()
        {
            return Service.GetAllRemindersAsync().Result.ToList();
        }
        public async Task<ReminderViewModel> CreateAsync(ReminderViewModel model)
        {
            var validationResult = Validate(model);
            if (string.IsNullOrEmpty(validationResult))
            {
                return await Service.CreateReminderAsync(model);
            }
            else
            {
                throw new ValidationException(validationResult);
            }
        }

        public string Validate(ReminderViewModel reminder)
        {
            string result = "";
            if (string.IsNullOrEmpty(reminder.Title))
            {
                result = "Title is required";
            }
            if (reminder.DateTime < DateTime.Now)
            {
                result = "DateTime must be in the future";
            }
            return result;
        }
    }
}
