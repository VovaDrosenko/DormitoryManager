using DormitoryManager.Models.DTO_s.Dormitory;
using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Models.Entities;
using DormitoryManager.Services;

namespace DormitoryManager.Interfaces
{
    public interface IDormitoryService
    {
        Task<List<DormitoryDto>> GettAll();
        Task<Dormitory> Get(int id);
        Task<ServiceResponse> GetByName(DormitoryDto model);
        Task Create(DormitoryDto model);
        Task Update(DormitoryDto model);
        Task Delete(int id);
    }
}
