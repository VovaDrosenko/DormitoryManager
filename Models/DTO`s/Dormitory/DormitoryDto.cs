using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.DTO_s.Dormitory;
//DM
public partial class DormitoryDto
{
    public int Id { get; set; }

    public string? DormNumber { get; set; }

    public string? Address { get; set; }

    public int? Floors { get; set; }/*

    public virtual ICollection<DormitoryComendant> DormitoryComendants { get; set; } = new List<DormitoryComendant>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Student> Student { get; set; } = new List<Student>();*/
}
