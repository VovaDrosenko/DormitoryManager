using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class Room
{
    public string RoomId { get; set; } = null!;

    public string DormId { get; set; } = null!;

    public int? NumberOfRoom { get; set; }

    public string? NumberOfBeds { get; set; }

    public string? ResidentsGender { get; set; }

    public virtual Dormitory Dorm { get; set; } = null!;

    public virtual ICollection<StudentRoom> StudentRooms { get; set; } = new List<StudentRoom>();
}
