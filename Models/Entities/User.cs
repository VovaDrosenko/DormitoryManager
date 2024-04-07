using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManager.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty ;
        public string UserMiddleName { get; set; } = string.Empty;

        // Foreign key for Room
        [ForeignKey("Room")]
        public int RoomID { get; set; }
        public required Room Room { get; set; }
        public int RoomNumber { get; set; }

        // Foreign key for Dormitory
        [ForeignKey("Dormitory")]
        public int DormitoryID { get; set; }
        public required Dormitory Dormitory { get; set; }
        public int DormitoryNumber { get; set; }
    }
}
