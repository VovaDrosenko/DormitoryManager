using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.Entities;

public partial class Worker
{
    [Key]
    public string WorkerId { get; set; } = null!;

    public string? WorkerName { get; set; }

    public string? WorkerSurname { get; set; }

    public string? WorkerLastname { get; set; }

    public string? WorkerPhone { get; set; }

    public string? WorkerEmail { get; set; }

    public string? WorkerPassword { get; set; }

    public string? RoleId { get; set; }

    public virtual DormitoryComendant? DormitoryComendant { get; set; }

    public virtual FacultyWorker? FacultyWorker { get; set; }

}
