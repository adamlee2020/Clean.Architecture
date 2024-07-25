using RingoMedia.DataAccess;
using RingoMedia.DataAccess.Entities;
using RingoMedia.Infrastructure.Interfaces; 

namespace RingoMedia.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
