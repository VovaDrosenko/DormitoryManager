using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManager.Models.DTO_s.Room;

public class RoomDto
{
    public int Id { get; set; }

    public int DormId { get; set; }

    public int NumberOfRoom { get; set; }

    public string? NumberOfBeds { get; set; }

    public string? ResidentsGender { get; set; }
}
