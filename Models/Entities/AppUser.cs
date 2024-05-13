using DormitoryManager.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManager.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DormId { get; set; } = string.Empty;
        public ICollection<Dormitory> Dormitory { get; set; } = new List<Dormitory>();
        public string FacultyId { get; set; } = string.Empty;
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}

