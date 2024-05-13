using Ardalis.Specification;
using DormitoryManager.Models.Entities;

namespace DormitoryManager.Specifications
{
    public class DormitorySpecification
    {
        public class GetByName : Specification<Dormitory>
        {
            public GetByName(string name)
            {
                Query.Where(x => x.DormNumber == name);
            }
        }
    }
}
