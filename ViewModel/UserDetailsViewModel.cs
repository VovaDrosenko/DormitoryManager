using DormitoryManager.Models.DTO_s.Dormitory;
using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Models.DTO_s.User;

namespace DormitoryManager.ViewModel {
    public class UserDetailsViewModel {
        public List<UsersDto> users { get; set; }
        public List<DormitoryDto>? dormitory { get; set; }
        public List<FacultiesDto>? faculties { get; set; }
    }
}
