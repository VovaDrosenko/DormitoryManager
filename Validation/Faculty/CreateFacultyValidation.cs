using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Validation.Student;
using FluentValidation;

namespace DormitoryManager.Validation.Faculty {
    public class CreateFacultyValidation: AbstractValidator<FacultiesDto> {
        public CreateFacultyValidation() {
                RuleFor(r => r.FacultyName).NotEmpty().MaximumLength(64).MinimumLength(2);
                RuleFor(r => r.FacultyAddress).NotEmpty().MaximumLength(64).MinimumLength(2);
        }
    }
}
