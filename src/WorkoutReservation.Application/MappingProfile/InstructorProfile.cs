using AutoMapper;
using WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.MappingProfile;

public class InstructorProfile : Profile
{
    public InstructorProfile()
    {
        CreateMap<Instructor, InstructorListQueryDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.Join(" ", src.FirstName, src.LastName)));

        CreateMap<Instructor, InstructorDetailQueryDto>();
        CreateMap<WorkoutType, WorkoutTypeForInstructorDetailQueryDto>();
    }
}
