﻿using AutoMapper;
using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Models.DTO_s.Student;

namespace DormitoryManager.AutoMapper
{
    public class AutoMapperFacultyProfile : Profile
    {
        public AutoMapperFacultyProfile()
        {
            CreateMap<FacultiesDto, Models.Entities.Faculty>().ReverseMap();
        }
    }
}
