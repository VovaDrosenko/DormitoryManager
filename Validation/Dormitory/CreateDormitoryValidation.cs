using DormitoryManager.Models.DTO_s.Dormitory;
using FluentValidation;

namespace DormitoryManager.Validation.Dormitory {
    public class CreateDormitoryValidation: AbstractValidator<DormitoryDto> {
        public CreateDormitoryValidation() {
            RuleFor(r => r.Address).NotEmpty().MaximumLength(100).MinimumLength(5);
            RuleFor(r => r.Floors).NotEmpty().NotNull().ExclusiveBetween(0,15);
            RuleFor(r => r.DormNumber).NotEmpty().MaximumLength(3).MinimumLength(1);
        }
    }
}
