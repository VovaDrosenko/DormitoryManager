
using AutoMapper;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Specifications;

namespace DormitoryManager.Services.Faculty
{
    public class FacultyService : IFacultyService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Models.Entities.Faculty> _repository;

        public FacultyService( IMapper mapper, IRepository<Models.Entities.Faculty> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Create(FacultiesDto model)
        {
            await _repository.Insert(_mapper.Map<Models.Entities.Faculty>(model));
            await _repository.Save();
        }

        public async Task Delete(int id)
        {
            var result = await Get(id);
            if (result != null)
            {
                await _repository.Delete(id);
                await _repository.Save();
            }
        }

        public async Task<Models.Entities.Faculty> Get(int id)
        {
            if (id < 0) return null;

            var faculty = await _repository.GetByID(id);
            if (faculty == null) return null;

            return faculty;
        }

        public async Task<ServiceResponse> GetByName(FacultiesDto model)
        {
            var result = await _repository.GetItemBySpec(new FacultySpecification.GetByName(model.FacultyName));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Category exists."
                };
            }
            var category = _mapper.Map<FacultiesDto>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Category successfully loaded.",
                Payload = category
            };
        }

        public async Task Update(FacultiesDto model)
        {
            await _repository.Update(_mapper.Map<Models.Entities.Faculty>(model));
            await _repository.Save();
        }

        async Task<List<FacultiesDto>> IFacultyService.GettAll()
        {
            var result = await _repository.GetAll();
            return _mapper.Map<List<FacultiesDto>>(result);
        }
    }
}
