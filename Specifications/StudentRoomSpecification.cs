using Ardalis.Specification;
using DormitoryManager.Models.Entities;

namespace DormitoryManager.Specifications
{
    public class StudentRoomSpecification
    {
        public class GetByStudentId : Specification<StudentRoom>
        {
            public GetByStudentId(int id)
            {
                Query.Where(x => x.StudentId== id);
            }
        }
    }
}
