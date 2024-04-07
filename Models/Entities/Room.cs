using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManager.Models.Entities
{
    [Table("Room")]
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomID { get; set; }

        public int RoomNumber { get; set; }
        public int NumberOfBeds { get; set; }

        // Foreign key for Dormitory
        [ForeignKey("Dormitory")]
        public int DormitoryID { get; set; }
        public required Dormitory Dormitory { get; set; }
    }
}
