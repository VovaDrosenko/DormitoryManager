using DormitoryManager.Models.DTO_s.Dormitory;
using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Models.DTO_s.User;
using DormitoryManager.Models.Entities;

namespace DormitoryManager.ViewModel {
    public class UserDetailsViewModel {
        public List<AppUser> users { get; set; }
        public List<DormitoryDto>? dormitory { get; set; }
        public List<FacultiesDto>? faculties { get; set; }
    }
}
