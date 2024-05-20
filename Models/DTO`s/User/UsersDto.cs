using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManager.Models.DTO_s.User
{
    public class UsersDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public string Email { get; set; } = string.Empty;
        public string LockedEnd { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string FacultyId { get; set; } = string.Empty;
        public string DormId { get; set; } = string.Empty;
    }
}
