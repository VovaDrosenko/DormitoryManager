using AutoMapper;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.DTO_s.Student;
using DormitoryManager.Models.Entities;
using DormitoryManager.Specifications;

namespace DormitoryManager.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Student> _repository;


        public StudentService(IMapper mapper, IRepository<Student> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task Create(CreateStudentDto model)
        {
            await _repository.Insert(_mapper.Map<Student>(model));
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

            var student = await _repository.GetByID(id);
            if (student == null) return null;

            return _mapper.Map<StudentsDto>(student);
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
            await _repository.Update(_mapper.Map<Student>(model));
            await _repository.Save();
        }

        public async Task<List<StudentsDto>> GettAllSettStud()
        {
            var result = await _repository.GetAll();
            IEnumerable<StudentsDto> query = from student in result
                                             where student.StatusId == 7
                                             select _mapper.Map<StudentsDto>(student);
            return _mapper.Map<List<StudentsDto>>(query);
        }

        public async Task<List<StudentsDto>> GetAllRequest()
        {
            var result = await _repository.GetAll();
            IEnumerable<StudentsDto> query = from student in result
                                             where student.StatusId == 5
                                             select _mapper.Map<StudentsDto>(student);
            return _mapper.Map<List<StudentsDto>>(query);
        }

        public async Task<List<StudentsDto>> GetAllInProgress()
        {
            var result = await _repository.GetAll();
            IEnumerable<StudentsDto> query = from student in result
                                             where student.StatusId == 6
                                             select _mapper.Map<StudentsDto>(student);
            return _mapper.Map<List<StudentsDto>>(query);
        }

        public async Task<int> GetOccupiedBedsCount(int roomId)
        {
            var studentsInRoom = await _repository.GetAll();

            var occupiedBedsCount = studentsInRoom.Where(s => s.RoomId == roomId).Count();

            return occupiedBedsCount;
        }
    }
}
