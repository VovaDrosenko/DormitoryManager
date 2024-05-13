using Ardalis.Specification;
using DormitoryManager.Models.Entities;

namespace DormitoryManager.Specifications
{
    public class RoomSpecification
    {
        public class GetByName : Specification<Room>
        {
            public GetByName(int name)
            {
                Query.Where(x => x.NumberOfRoom == name);
            }
        }
    }
}
