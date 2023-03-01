using AutoMapper;
using WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class InstructorProfile : Profile
{
    public InstructorProfile()
    {
        CreateMap<Instructor, InstructorListQueryDto>();

        CreateMap<Instructor, InstructorDetailQueryDto>();
        CreateMap<WorkoutType, WorkoutTypeForInstructorDetailQueryDto>();

        CreateMap<CreateInstructorCommand, Instructor>();
    }
}
