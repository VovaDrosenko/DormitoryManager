using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormitoryManager.Models.DTO_s.Student;

namespace DormitoryManager.Validation.Student
{
    public class CreateStudentValidation : AbstractValidator<StudentsDto>
    {
        public CreateStudentValidation()
        {
            RuleFor(r => r.StudentName).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.StudentLastname).NotEmpty().MaximumLength(64).MinimumLength(2);
        }
    }
}
