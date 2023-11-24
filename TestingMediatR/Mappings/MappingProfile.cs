using AutoMapper;
using TestingMediatR.Models;
using TestingMediatR.Queries.GetStudentByIdQuery;

namespace TestingMediatR.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDetails, StudentQueryResponse>()
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade.Grade))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.StudentRoles.RoleName));
        }
    }
}
