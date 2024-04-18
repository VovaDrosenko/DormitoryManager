﻿using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class Faculty
{
    public string FacultyId { get; set; } = null!;

    public string? FacultyName { get; set; }

    public string? FacultyAddress { get; set; }

    public virtual ICollection<DeanWorker> DeanWorkers { get; set; } = new List<DeanWorker>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Dormitory> Dorms { get; set; } = new List<Dormitory>();
}