using DormitoryManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.Entities;

public partial class Room: IEntity
{
    public int Id { get; set; }

    public int DormId { get; set; }

    public int NumberOfRoom { get; set; }

    public string? NumberOfBeds { get; set; }

    public string? ResidentsGender { get; set; }
    public int? FreeBeds { get; set; }
    public int Floor { get; set; }

    public virtual Dormitory Dorm { get; set; } = null!;
}
