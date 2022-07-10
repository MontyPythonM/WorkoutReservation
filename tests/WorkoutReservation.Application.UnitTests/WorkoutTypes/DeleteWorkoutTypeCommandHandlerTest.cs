using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;
using WorkoutReservation.Application.MappingProfile;
using WorkoutReservation.Application.UnitTests.Mocks;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.UnitTests.WorkoutTypes
{
    public class DeleteWorkoutTypeCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<DeleteWorkoutTypeCommandHandler>> _mockLogger;
        private readonly Mock<IWorkoutTypeRepository> _mockWorkoutTypeRepository;
        private readonly List<WorkoutType> _workoutTypeDummyList;

        public DeleteWorkoutTypeCommandHandlerTest()
        {
            _mockWorkoutTypeRepository = WorkoutTypeRepositoryMock.GetRepositoryMock();
            _workoutTypeDummyList = WorkoutTypeRepositoryMock.GetDummyList();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WorkoutTypeProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
            _mockLogger = new Mock<ILogger<DeleteWorkoutTypeCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ForValidWorkoutTypeId_RemoveWorkoutType()
        {
            // arrange
            var deleteHandler = new DeleteWorkoutTypeCommandHandler(_mockWorkoutTypeRepository.Object, _mockLogger.Object);

            var workoutTypesBeforeCount = _workoutTypeDummyList.Count;

            // act
            await deleteHandler.Handle(new DeleteWorkoutTypeCommand
            {
                WorkoutTypeId = workoutTypesBeforeCount
            }, CancellationToken.None);

            var allWorkoutTypesAfterCount = (await _mockWorkoutTypeRepository.Object.GetAllAsync()).Count;

            // assert
            allWorkoutTypesAfterCount.Should().Be(workoutTypesBeforeCount - 1);
        }

        [Fact]
        public async Task Handle_ForInvalidWorkoutTypeId_ThrowNotFoundException()
        {
            // arrange
            var deleteHandler = new DeleteWorkoutTypeCommandHandler(_mockWorkoutTypeRepository.Object, _mockLogger.Object);

            var notExistWorkoutType = _workoutTypeDummyList.Count + 1;

            // act
            Func<Task> result = async () => await deleteHandler.Handle(new DeleteWorkoutTypeCommand { WorkoutTypeId = notExistWorkoutType }, CancellationToken.None);

            // assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}
