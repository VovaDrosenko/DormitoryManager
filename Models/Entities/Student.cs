using DormitoryManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.Entities;

public partial class Student : IEntity
{
    public int Id { get; set; }

    public string? StudentName { get; set; } = null!;

    public string? StudentMiddlename { get; set; }=null!;

    public string? StudentLastname { get; set; }= null!;

    public string? StudentPhone { get; set; } = null!;

    public string? StudentEmail { get; set; } = null!;
    public byte[] Photo { get;set; } = null!;
    public byte[] ApplicationScan { get; set; } = null!;
    public int RoomId { get; set; }


    public int? Course { get; set; }
    public DateTime? DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }
    
    public int StatusId { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; } = null!;

    public int FacultyId { get; set; }

    public virtual Faculty? Faculty { get; set; }
    public virtual Status? Status { get; set; }
    public virtual Room? Room { get; set; }

}
