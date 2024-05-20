using DormitoryManager.Models.DTO_s.StudentRoom;
using DormitoryManager.Services;

namespace DormitoryManager.Interfaces
{
    public interface IStudentRoomService
    {
        Task<List<StudentRoomDto>> GetAll();
        Task<StudentRoomDto> Get(int id);
        Task Create(StudentRoomDto model);
        Task Update(StudentRoomDto model);
        Task Delete(int id);
    }
}
