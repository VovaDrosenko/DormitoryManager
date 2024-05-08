using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DormitoryManager.Models.DTO_s.User
{
    public class LoginUserDto
    {
        public string Email { get; set;} = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
