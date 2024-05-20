using DormitoryManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManager.Models.DTO_s.Student
{
    public class UpdateStudentsDto
    {
        public int Id {  get; set; }
        public int DormitoryId {  get; set; }
        public int RoomId { get; set; }
        public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }
    }
}
