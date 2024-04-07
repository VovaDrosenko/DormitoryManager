using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManager.Models.Entities
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomID { get; set; }

        public int RoomNumber { get; set; }
        public int NumberOfBeds { get; set; }

        // Foreign key for Dormitory
        public int DormitoryID { get; set; }
        //[ForeignKey("DormitoryID")]
        public Dormitory Dormitory { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
