using DormitoryManager.Models.DTO_s.Student;
using DormitoryManager.Services;
using DormitoryManager.Services.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManager.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentsDto>> GettAll();
        Task<StudentsDto> Get(int id);
        Task<ServiceResponse> GetByName(StudentsDto model);
        Task Create(StudentsDto model);
        Task Update(StudentsDto model);
        Task Delete(int id);
    }
}
