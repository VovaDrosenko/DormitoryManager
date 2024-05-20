using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryManager.Models.DTO_s.Student;

namespace DormitoryManager.Validation.Student
{
    public class UpdateStudentsValidation : AbstractValidator<StudentsDto>
    {
        public UpdateStudentsValidation()
        {
            RuleFor(r => r.DormitoryId).NotEmpty();
            RuleFor(r => r.RoomId).NotEmpty();
        }
    }
}
