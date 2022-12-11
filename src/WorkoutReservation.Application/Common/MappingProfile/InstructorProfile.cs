using AutoMapper;
using WorkoutReservation.Application.Features.Instructors.Commands.CreateInstructor;
using WorkoutReservation.Application.Features.Instructors.Commands.UpdateInstructor;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorDetail;
using WorkoutReservation.Application.Features.Instructors.Queries.GetInstructorList;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.Common.MappingProfile;

public class InstructorProfile : Profile
{
    public InstructorProfile()
    {
        // InstructorListQueryDto
        CreateMap<Instructor, InstructorListQueryDto>();

        // InstructorDetailQueryDto
        CreateMap<Instructor, InstructorDetailQueryDto>();
        CreateMap<WorkoutType, WorkoutTypeForInstructorDetailQueryDto>();

        // CreateInstructorCommandHandler
        CreateMap<CreateInstructorCommand, Instructor>();

        // UpdateInstructorCommandHandler
        CreateMap<UpdateInstructorCommand, Instructor>()
            .ForMember(x => x.Id, y => y.MapFrom(z => z.InstructorId));
    }
}
