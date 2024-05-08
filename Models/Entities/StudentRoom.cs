using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.Entities;

public partial class StudentRoom
{
    public int Id {  get; set; }
    public int RoomId { get; set; }

    public int DormId { get; set; }

    public int StudentId { get; set; }

    public DateTime? DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
