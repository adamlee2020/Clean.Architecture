using RingoMedia.DataAccess;
using RingoMedia.DataAccess.Entities;
using RingoMedia.Infrastructure.Interfaces;

namespace RingoMedia.Infrastructure.Repositories
{
    public class ReminderRepository : Repository<Reminder>, IReminderRepository
    {
        public ReminderRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
