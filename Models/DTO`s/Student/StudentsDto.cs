using DormitoryManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManager.Models.DTO_s.Student
{
    public class StudentsDto
    {
        public int Id { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string StudentMiddlename { get; set; } = string.Empty;
        public string StudentLastname { get; set; } = string.Empty;
        public string StudentPhone { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public int FacultyId { get; set; }
        public Entities.Faculty faculty { get; set; } = new Entities.Faculty();
        public string Wishes { get; set; } = string.Empty;
        public IFormFile Photo { get; set; }
        public IFormFile ApplicationScan { get; set; }
    }
}
