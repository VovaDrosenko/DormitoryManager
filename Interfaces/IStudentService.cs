using DormitoryManager.Models.DTO_s.Student;
using DormitoryManager.Services;

namespace DormitoryManager.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentsDto>> GettAllSettStud();
        Task<List<StudentsDto>> GetAllRequest();
        Task<StudentsDto> Get(int id);
        Task<ServiceResponse> GetByName(StudentsDto model);
        Task Create(CreateStudentDto model);
        Task Update(StudentsDto model);
        Task Delete(int id);
    }
}
