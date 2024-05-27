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
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int? StatusId { get; set; }
        public string StudentEmail { get; set; } = string.Empty;
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = string.Empty;
        public int DormitoryId { get; set; }
        public string DormNum { get; set; } = string.Empty;
        public int RoomId { get; set; }
        public Entities.Room Room { get; set; } = null!;
        public int RoomNum { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoString { get; set; }
        public byte[] ApplicationScan { get; set; }
        public string ApplicationScanString { get; set; }
    }
}
