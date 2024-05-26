using DormitoryManager.Models.DTO_s.Dormitory;
using DormitoryManager.Models.DTO_s.Room;
using DormitoryManager.Models.Entities;

namespace DormitoryManager.ViewModel {
    public class RoomsViewModel {
        public RoomDto? Room { get; set; }
        public List<RoomDto>? Rooms { get; set; } = null;
        public List<DormitoryDto>? Dormitories { get; set; } = null;
        public int dormitoryId { get; set; }
        public int? SelectedFloor { get; set; } = null;
        public IEnumerable<int> Floors { get; set; } = null;
        public Dictionary<int, int?> FreeBedsPerFloor { get; set; }
    }
}
