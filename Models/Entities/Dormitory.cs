using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManager.Models.Entities
{
    [Table("Dormitory")]
    public class Dormitory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DormitoryID { get; set; }

        public int DormitoryNumber { get; set; }

        // Navigation property for one-to-many relationship with Room
        public required ICollection<Room> Rooms { get; set; }
    }
}
