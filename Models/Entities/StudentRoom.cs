using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class StudentRoom
{
    public string RoomId { get; set; } = null!;

    public string DormId { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public DateTime? DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
