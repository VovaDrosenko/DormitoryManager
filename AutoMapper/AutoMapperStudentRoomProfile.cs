using AutoMapper;
using DormitoryManager.Models.DTO_s.StudentRoom;

namespace DormitoryManager.AutoMapper
{
    public class AutoMapperStudentRoomProfile : Profile
    {
        public AutoMapperStudentRoomProfile()
        {
            CreateMap<StudentRoomDto, Models.Entities.StudentRoom>().ReverseMap();

        }
    }
}

