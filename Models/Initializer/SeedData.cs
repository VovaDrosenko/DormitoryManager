//using DormitoryManager.Models.Entities;
//
//namespace DormitoryManager.Models.Initializer
//{
//    public static class SeedData
//    {
//        public static void Initialize(DormitoryManagerContext context)
//        {
//            if (!context.Dormitories.Any())
//            {
//                context.Dormitories.AddRange(
//                    new Dormitory { DormId = "3", DormNumber = "101", Address = "123 Main St", Floors = 5 },
//                    new Dormitory { DormId = "4", DormNumber = "102", Address = "456 Elm St", Floors = 4 },
//                    new Dormitory { DormId = "5", DormNumber = "102", Address = "456 Elm St", Floors = 4 }
//                );
//                context.SaveChanges();
//            }
//        }
//    }
//
//}
