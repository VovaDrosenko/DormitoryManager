using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.DTO_s.StudentRoom;

public class StudentRoomDto
{
    public int Id {  get; set; }
    public int RoomId { get; set; }

    public int DormId { get; set; }

    public int StudentId { get; set; }

    public DateTime? DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }
}
