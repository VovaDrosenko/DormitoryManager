using AutoMapper;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.DTO_s.StudentRoom;
using DormitoryManager.Models.Entities;
using DormitoryManager.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManager.Services
{
    public class StudentRoomService : IStudentRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<StudentRoom> _repository;
        private readonly IRepository<Student> _stdRepository;


        public StudentRoomService(IMapper mapper, IRepository<StudentRoom> repository, IRepository<Student> stdRepositort)
        {
            _mapper = mapper;
            _repository = repository;
            _stdRepository = stdRepositort;
        }
        public async Task Create(StudentRoomDto model)
        {
            await _repository.Insert(_mapper.Map<StudentRoom>(model));
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

        public async Task<StudentRoomDto> Get(int id)
        {
            if (id < 0) return null;

            var studentRoomSpec = new StudentRoomSpecification.GetByStudentId(id);

            // Get the student room details by student ID using the specification
            var studentRoom = await _repository.GetItemBySpec(studentRoomSpec);
            if (studentRoom == null) return null;

            // Map student and student room details to DTO
            var studentRoomDto = _mapper.Map<StudentRoomDto>(studentRoom);

            return studentRoomDto;
        }


        public async Task Update(StudentRoomDto model)
        {

            await _repository.Update(_mapper.Map<StudentRoom>(model));
            await _repository.Save();
        }

        public async Task<List<StudentRoomDto>> GetAll()
        {
            var result = await _repository.GetAll();
           
            return _mapper.Map<List<StudentRoomDto>>(result);
        }

    }
}
