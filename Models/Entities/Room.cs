using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.Entities;

public partial class Room
{
    public int Id { get; set; }

    public int DormId { get; set; }

    public int? NumberOfRoom { get; set; }

    public string? NumberOfBeds { get; set; }

    public string? ResidentsGender { get; set; }

    public virtual Dormitory Dorm { get; set; } = null!;

    public virtual ICollection<StudentRoom> StudentRooms { get; set; } = new List<StudentRoom>();
}
