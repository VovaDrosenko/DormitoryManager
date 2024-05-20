using AutoMapper;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.DTO_s.Dormitory;
using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Models.DTO_s.User;
using DormitoryManager.Services;

namespace DormitoryManager.ViewModel {
    public class UserViewModel {
        public CreateUserDto? user { get; set; }
        public List<DormitoryDto>? dormitory { get; set; }
        public List<FacultiesDto>? faculties { get; set; }
    }
}
