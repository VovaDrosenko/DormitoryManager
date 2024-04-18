using System;
using System.Collections.Generic;

namespace DormitoryManager.Models.Entities;

public partial class Role
{
    public string RoleId { get; set; } = null!;

    public string? RoleName { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
