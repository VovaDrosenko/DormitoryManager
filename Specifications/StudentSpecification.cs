using Ardalis.Specification;
using DormitoryManager.Models.Entities;

namespace DormitoryManager.Specifications
{
    public class StudentSpecification
    {
        public class GetByName : Specification<Student>
        {
            public GetByName(string name)
            {
                Query.Where(x => x.StudentLastname == name);
            }
        }
    }
}
