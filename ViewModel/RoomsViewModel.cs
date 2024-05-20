using DormitoryManager.Models.DTO_s.Room;
using DormitoryManager.Models.Entities;

namespace DormitoryManager.ViewModel {
    public class RoomsViewModel {
        public RoomDto? Room { get; set; }
        public List<RoomDto>? Rooms { get; set; }
        public int dormitoryId { get; set; }
    }
}
