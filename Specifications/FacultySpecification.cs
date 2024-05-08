using Ardalis.Specification;
using DormitoryManager.Models.Entities;

namespace DormitoryManager.Specifications
{
    public class FacultySpecification
    {
        public class GetByName : Specification<Faculty>
        {
            public GetByName(string name)
            {
                Query.Where(x => x.FacultyName == name);
            }
        }
    }
}
