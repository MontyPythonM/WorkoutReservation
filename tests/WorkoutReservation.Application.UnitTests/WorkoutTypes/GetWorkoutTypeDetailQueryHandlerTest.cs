using AutoMapper;
using FluentAssertions;
using Moq;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypeDetail;
using WorkoutReservation.Application.MappingProfile;
using WorkoutReservation.Application.UnitTests.Mocks;
using WorkoutReservation.Domain.Entities;
using Xunit;

namespace WorkoutReservation.Application.UnitTests.WorkoutTypes;

public  class GetWorkoutTypeDetailQueryHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IWorkoutTypeRepository> _mockWorkoutTypeRepository;
    private readonly List<WorkoutType> _workoutTypeDummyList;

    public GetWorkoutTypeDetailQueryHandlerTest()
    {
        _mockWorkoutTypeRepository = WorkoutTypeRepositoryMock.GetRepositoryMock();
        _workoutTypeDummyList = WorkoutTypeRepositoryMock.GetDummyList();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WorkoutTypeProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task Handle_WorkoutTypeExist_GetCorrectResult()
    {
        // arrange
        var handler= new GetWorkoutTypeDetailQueryHandler(_mockWorkoutTypeRepository.Object, _mapper);

        var dummyWorkoutType = _workoutTypeDummyList.First();
        
        // act
        var result = await handler.Handle(new GetWorkoutTypeDetailQuery { WorkoutTypeId = dummyWorkoutType.Id }, CancellationToken.None);

        // assert
        result.Should().NotBeNull();
        result.Id.Should().Be(dummyWorkoutType.Id);   
        result.WorkoutTypeTags.Should().HaveCount(dummyWorkoutType.WorkoutTypeTags.Count);
    }

    [Fact]
    public async Task Handle_WorkoutTypeNotExist_ThrowNotFoundException()
    {
        // arrange
        var handler = new GetWorkoutTypeDetailQueryHandler(_mockWorkoutTypeRepository.Object, _mapper);
        var notExistWorkoutType = _workoutTypeDummyList.Count + 1;

        // act
        Func<Task> result = async () => await handler.Handle(new GetWorkoutTypeDetailQuery { WorkoutTypeId = notExistWorkoutType }, CancellationToken.None);

        // assert
        await result.Should().ThrowAsync<NotFoundException>();
    }
}
