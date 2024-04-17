using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class DeanWorker
{
    public string DeanWorkerId { get; set; } = null!;

    public string? FacultyId { get; set; }

    public string? DeanWorkerName { get; set; }

    public string? DeanWorkerMiddlename { get; set; }

    public string? DeanWorkerLastname { get; set; }

    public string? DeanWorkerPhone { get; set; }

    public string? DeanWorkerEmail { get; set; }

    public string? DeanWorkerPassword { get; set; }

    public virtual Faculty? Faculty { get; set; }
}
