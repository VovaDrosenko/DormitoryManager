﻿using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Models.Entities;
using DormitoryManager.Services;

namespace DormitoryManager.Interfaces
{
    public interface IFacultyService
    {
        Task<List<FacultiesDto>> GettAll();
        Task<Faculty> Get(int id);
        Task<ServiceResponse> GetByName(FacultiesDto model);
        Task Create(FacultiesDto model);
        Task Update(FacultiesDto model);
        Task Delete(int id);
    }
}
