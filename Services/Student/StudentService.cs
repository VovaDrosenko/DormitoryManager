using AutoMapper;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.DTO_s.Student;
using DormitoryManager.Specifications;

namespace DormitoryManager.Services.Student
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Models.Entities.Student> _repository;

        public StudentService( IMapper mapper, IRepository<Models.Entities.Student> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Create(StudentsDto model)
        {
            await _repository.Insert(_mapper.Map<Models.Entities.Student>(model));
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

        public async Task<StudentsDto> Get(int id)
        {
            if (id < 0) return null;

            var category = await _repository.GetByID(id);
            if (category == null) return null;

            return _mapper.Map<StudentsDto>(category);
        }

        public async Task<ServiceResponse> GetByName(StudentsDto model)
        {
            var result = await _repository.GetItemBySpec(new StudentSpecification.GetByName(model.StudentLastname));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Category exists."
                };
            }
            var category = _mapper.Map<StudentsDto>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Category successfully loaded.",
                Payload = category
            };
        }

        public async Task Update(StudentsDto model)
        {
            await _repository.Update(_mapper.Map<Models.Entities.Student>(model));
            await _repository.Save();
        }

         public async Task<List<StudentsDto>> GettAll()
        {
            var result = await _repository.GetAll();
            return _mapper.Map<List<StudentsDto>>(result);
        }
    }
}
