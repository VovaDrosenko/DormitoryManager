using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class DormitoryComendant
{
    public string WorkerId { get; set; } = null!;

    public string? DormId { get; set; }

    public virtual Dormitory? Dorm { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
