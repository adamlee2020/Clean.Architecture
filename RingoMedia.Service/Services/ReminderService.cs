using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RingoMedia.DataAccess.Entities;
using RingoMedia.DataAccess.ViewModels;
using RingoMedia.Infrastructure.Interfaces;
using RingoMedia.Infrastructure.Repositories;

namespace RingoMedia.Service.Services
{
    public class ReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IMapper _mapper;

        public ReminderService(IReminderRepository reminderRepository, IMapper mapper)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ReminderViewModel>> GetAllRemindersAsync()
        {
            var reminders = await _reminderRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<ReminderViewModel>>(reminders);
        }

        public async Task<ReminderViewModel> CreateReminderAsync(ReminderViewModel reminderViewModel)
        {
            var reminder = _mapper.Map<Reminder>(reminderViewModel);
            await _reminderRepository.AddAsync(reminder);
            return _mapper.Map<ReminderViewModel>(reminder);
        }

    }
}
