using FluentValidation;
using DormitoryManager.Models.DTO_s.User;

namespace DormitoryManager.Valudation.User
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidation()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.OldPassword).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(r => r.ConfirmPassword).MinimumLength(6).Equal(r => r.Password);
        }
    }
}
