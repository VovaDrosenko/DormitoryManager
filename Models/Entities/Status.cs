using DormitoryManager.Interfaces;

namespace DormitoryManager.Models.Entities {
    public class Status: IEntity {
        public int Id { get; set; }
        public string? StatusName { get; set; }
    }
}
