﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.Entities;

public partial class Faculty
{
    [Key]
    public string FacultyId { get; set; } = null!;

    public string? FacultyName { get; set; }

    public string? FacultyAddress { get; set; }

    public virtual ICollection<FacultyWorker> FacultyWorkers { get; set; } = new List<FacultyWorker>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Dormitory> Dorms { get; set; } = new List<Dormitory>();
}
