using DormitoryManager.Models.DTO_s.Room;
using DormitoryManager.Models.Entities;
using DormitoryManager.Services;

namespace DormitoryManager.Interfaces
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GettAll();
        Task<Room> Get(int id);
        Task<ServiceResponse> GetByName(RoomDto model);
        Task Create(RoomDto model);
        Task Update(RoomDto model);
        Task Delete(int id);
    }
}
