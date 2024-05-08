using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.Entities;

public partial class DormitoryComendant
{
    public int Id { get; set; }
    public int WorkerId { get; set; }

    public string? DormId { get; set; }

    public virtual Dormitory? Dorm { get; set; }

    public virtual Worker Worker { get; set; } = null!;
}
