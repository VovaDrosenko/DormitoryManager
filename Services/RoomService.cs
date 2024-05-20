using AutoMapper;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.DTO_s.Room;
using DormitoryManager.Specifications;

namespace DormitoryManager.Services
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Models.Entities.Room> _repository;

        public RoomService(IMapper mapper, IRepository<Models.Entities.Room> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Create(RoomDto model)
        {
            await _repository.Insert(_mapper.Map<Models.Entities.Room>(model));
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

        public async Task<Models.Entities.Room> Get(int id)
        {
            if (id < 0) return null;

            var faculty = await _repository.GetByID(id);
            if (faculty == null) return null;

            return faculty;
        }

        public async Task<ServiceResponse> GetByName(RoomDto model)
        {
            var result = await _repository.GetItemBySpec(new RoomSpecification.GetByName(model.NumberOfRoom));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Category exists."
                };
            }
            var category = _mapper.Map<RoomDto>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Category successfully loaded.",
                Payload = category
            };
        }

        public async Task Update(RoomDto model)
        {
            await _repository.Update(_mapper.Map<Models.Entities.Room>(model));
            await _repository.Save();
        }

        async Task<List<RoomDto>> IRoomService.GettAll()
        {
            var result = await _repository.GetAll();
            return _mapper.Map<List<RoomDto>>(result);
        }
        async Task<List<RoomDto>> IRoomService.GettAllInDorm(int dormId) {
            var result = await _repository.GetAll(); // Отримуємо всі кімнати з бази даних
            var roomsInDorm = result.Where(r => r.DormId == dormId).ToList(); // Відбираємо кімнати, які належать до переданого гуртожитку
            return _mapper.Map<List<RoomDto>>(roomsInDorm); // Повертаємо список кімнат, змінений на RoomDto
        }
        async Task<RoomDto> IRoomService.GetByNumberOfRoom(int numberOfRoom){
            RoomDto room = new RoomDto();
            var rooms = await _repository.GetAll();
            foreach(var r in rooms) {
                if (r.NumberOfRoom == numberOfRoom)
                    room = _mapper.Map<RoomDto>(r);
            }
            return room;
        }

    }
}
