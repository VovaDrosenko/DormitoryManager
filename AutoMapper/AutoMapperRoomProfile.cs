using AutoMapper;
using DormitoryManager.Models.DTO_s.Dormitory;
using DormitoryManager.Models.DTO_s.Room;

namespace DormitoryManager.AutoMapper
{
    public class AutoMapperRoomProfile : Profile
    {
        public AutoMapperRoomProfile()
        {
            CreateMap<RoomDto, Models.Entities.Room>().ReverseMap();
        }
    }
}
