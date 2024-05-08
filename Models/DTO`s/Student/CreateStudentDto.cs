using DormitoryManager.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.DTO_s.Student;

    public class CreateStudentDto
    {
    public string? StudentName { get; set; }

    public string? StudentLastname { get; set; }

    public string? StudentMiddlename { get; set; }

    public string? StudentPhone { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? StudentEmail { get; set; }

    public string? Gender { get; set; }

    public virtual Entities.Student? Faculty { get; set; }

}

