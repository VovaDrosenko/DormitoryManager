using DormitoryManager.Models.DTO_s.Room;
using DormitoryManager.Models.Entities;
using DormitoryManager.Services;

namespace DormitoryManager.Interfaces
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GettAll();
        Task<List<RoomDto>> GettAllInDorm(int dormId);
        Task<Room> Get(int id);
        Task<ServiceResponse> GetByName(RoomDto model);
        Task Create(RoomDto model);
        Task Update(RoomDto model);
        Task Delete(int id);
        Task<RoomDto> GetByNumberOfRoom(int numberOfRoom);
        Task<IEnumerable<RoomDto>> GetByDormitoryIdAndGender(int dormitoryId, string gender);
    }
}
