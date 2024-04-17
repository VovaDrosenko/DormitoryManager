using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class Dormitory
{
    public string? DormId { get; set; } = null;

    public string? DormNumber { get; set; }

    public string? Address { get; set; }

    public int? Floors { get; set; }

    public virtual ICollection<Comendant> Comendants { get; set; } = new List<Comendant>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
