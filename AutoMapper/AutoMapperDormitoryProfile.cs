using AutoMapper;
using DormitoryManager.Models.DTO_s.Dormitory;

namespace DormitoryManager.AutoMapper
{
    public class AutoMapperDormitoryProfile : Profile
    {
        public AutoMapperDormitoryProfile()
        {
            CreateMap<DormitoryDto, Models.Entities.Dormitory>().ReverseMap();
        }
    }
}
