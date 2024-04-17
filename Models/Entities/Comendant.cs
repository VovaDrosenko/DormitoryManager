using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class Comendant
{
    public string ComendantId { get; set; } = null!;

    public string? ComendantName { get; set; }

    public string? ComendantMiddlename { get; set; } = null;

    public string? ComendantLastname { get; set; } = null;

    public string? ComendantPhone { get; set; } = null;

    public string? ComendantEmail { get; set; } = null;

    public string? ComendantPassword { get; set; } = null;

    public string? DormId { get; set; } = null;

    public virtual Dormitory? Dorm { get; set; } = null;
}
