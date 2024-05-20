using DormitoryManager.Models.DTO_s.Room;
using FluentValidation;

namespace DormitoryManager.Validation.Room {
    public class CreateRoomValidation: AbstractValidator<RoomDto> {
        public CreateRoomValidation() {
            RuleFor(r=>r.NumberOfRoom).NotEmpty().InclusiveBetween(100,999);
            RuleFor(r => r.ResidentsGender).NotEmpty();
            RuleFor(r=>r.NumberOfBeds).NotEmpty().MinimumLength(1).MaximumLength(2);
        }
    }
}
