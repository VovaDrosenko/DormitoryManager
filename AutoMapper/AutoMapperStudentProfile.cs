using AutoMapper;
using DormitoryManager.Models.DTO_s.Student;

namespace DormitoryManager.AutoMapper
{
    public class AutoMapperStudentProfile : Profile
    {
        public AutoMapperStudentProfile()
        {
            CreateMap<StudentsDto, Models.Entities.Student>().ReverseMap();
            CreateMap<CreateStudentDto, Models.Entities.Student>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => ConvertToByteArray(src.Photo)))
                .ForMember(dest => dest.ApplicationScan, opt => opt.MapFrom(src => ConvertToByteArray(src.ApplicationScan)));
            CreateMap<Models.Entities.Student, StudentsDto>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => ConvertToIFormFile(src.Photo, "photo.jpg", "image/jpeg")))
                .ForMember(dest => dest.ApplicationScan, opt => opt.MapFrom(src => ConvertToIFormFile(src.ApplicationScan, "photo.jpg", "image/jpeg")));

        }

        private byte[] ConvertToByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private IFormFile ConvertToIFormFile(byte[] fileBytes, string fileName, string contentType)
        {
            // Create a memory stream from the byte array
            var stream = new MemoryStream(fileBytes);

            // Create a new instance of FormFile
            var formFile = new FormFile(stream, 0, fileBytes.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };

            return formFile;
        }
    }
}

