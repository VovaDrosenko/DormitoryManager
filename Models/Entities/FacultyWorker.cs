using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class FacultyWorker
{
    public string WorkerId { get; set; } = null!;

    public string? FacultyId { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
