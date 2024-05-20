
using AutoMapper;
using DormitoryManager.Interfaces;
using DormitoryManager.Models.DTO_s.Dormitory;
using DormitoryManager.Models.DTO_s.Faculty;
using DormitoryManager.Models.Entities;
using DormitoryManager.Specifications;

namespace DormitoryManager.Services
{
    public class DormitoryService : IDormitoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Models.Entities.Dormitory> _repository;

        public DormitoryService(IMapper mapper, IRepository<Models.Entities.Dormitory> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Create(DormitoryDto model)
        {
            await _repository.Insert(_mapper.Map<Models.Entities.Dormitory>(model));
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

        public async Task<Dormitory> Get(int id)
        {
            if (id < 0) return null;

            var dormitory = await _repository.GetByID(id);
            if (dormitory == null) return null;

            return dormitory;
        }

        public async Task<ServiceResponse> GetByName(DormitoryDto model)
        {
            var result = await _repository.GetItemBySpec(new DormitorySpecification.GetByName(model.DormNumber));
            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Category exists."
                };
            }
            var category = _mapper.Map<DormitoryDto>(result);
            return new ServiceResponse
            {
                Success = true,
                Message = "Category successfully loaded.",
                Payload = category
            };
        }

        public async Task Update(Dormitory model)
        {
            await _repository.Update(model);
            await _repository.Save();
        }
        public async Task UpdateDto(DormitoryDto model) {
            await _repository.Update(_mapper.Map<Models.Entities.Dormitory>(model));
            await _repository.Save();
        }

        async Task<List<DormitoryDto>> IDormitoryService.GettAll()
        {
            var result = await _repository.GetAll();
            return _mapper.Map<List<DormitoryDto>>(result);
        }
    }
}
