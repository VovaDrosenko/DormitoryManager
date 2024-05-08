using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.Entities;

public partial class Student
{
    [Key]
    public string StudentId { get; set; } = null!;

    public string? StudentName { get; set; }

    public string? StudentMiddlename { get; set; }

    public string? StudentLastname { get; set; }

    public string? StudentPhone { get; set; }

    public string? StudentEmail { get; set; }

    public int? Course { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? FacultyId { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<StudentRoom> StudentRooms { get; set; } = new List<StudentRoom>();
}
