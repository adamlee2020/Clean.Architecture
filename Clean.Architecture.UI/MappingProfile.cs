using AutoMapper;
using RingoMedia.DataAccess.Entities;
using RingoMedia.DataAccess.ViewModels;

namespace RingoMedia.UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<Reminder, ReminderViewModel>().ReverseMap();
        }
    }
}
