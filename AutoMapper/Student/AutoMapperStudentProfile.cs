using AutoMapper;
using DormitoryManager.Models.DTO_s.Student;

namespace DormitoryManager.AutoMapper.Student
{
    public class AutoMapperStudentProfile : Profile
    {
        public AutoMapperStudentProfile()
        {
            CreateMap<StudentsDto, Models.Entities.Student>().ReverseMap();
        }
    }
}
