using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManager.Models.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string UserMiddleName { get; set; } = string.Empty;

        // Foreign key for Room
        public int RoomID { get; set; }
        //[ForeignKey("RoomID")]
        public Room Room { get; set; }
    }
}
