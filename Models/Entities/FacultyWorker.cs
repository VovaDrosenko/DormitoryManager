using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.Entities;

public partial class FacultyWorker
{
    public int Id { get; set; }

    public string? FacultyId { get; set; }
    public int WorkerId { get; set; }

    public virtual Student? Faculty { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
