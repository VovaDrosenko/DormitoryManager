using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class Worker
{
    public int Id { get; set; }

    public string? WorkerName { get; set; }

    public string? WorkerSurname { get; set; }

    public string? WorkerLastname { get; set; }

    public string? WorkerPhone { get; set; }

    public string? WorkerEmail { get; set; }

    public string? WorkerPassword { get; set; }

    public virtual DormitoryComendant? DormitoryComendant { get; set; }

    public virtual FacultyWorker? FacultyWorker { get; set; }

}
