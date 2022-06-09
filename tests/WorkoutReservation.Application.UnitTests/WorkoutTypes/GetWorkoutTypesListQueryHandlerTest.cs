using AutoMapper;
using FluentAssertions;
using Moq;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Exceptions;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.DeleteWorkoutType;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
using WorkoutReservation.Application.MappingProfile;
using WorkoutReservation.Application.UnitTests.Mocks;
using WorkoutReservation.Domain.Entities;

namespace WorkoutReservation.Application.UnitTests.WorkoutTypes
{
    public class GetWorkoutTypesListQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IWorkoutTypeRepository> _mockWorkoutTypeRepository;
        private readonly List<WorkoutType> _workoutTypeDummyList;

        public GetWorkoutTypesListQueryHandlerTest()
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
        public async Task Handle_ForValidRequest_GetCorrectResults()
        {           
            // arrange
            var handler = new GetWorkoutTypesListQueryHandler(_mockWorkoutTypeRepository.Object, _mapper);

            var workoutTypesCount = _workoutTypeDummyList.Count;

            // act
            var result = await handler.Handle(new GetWorkoutTypesListQuery(), CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.Count.Should().Be(workoutTypesCount);
        }

        [Fact]
        public async Task Handle_ForEmptyRepository_ThrowNotFoundException()
        {
            // arrange
            var getHandler = new GetWorkoutTypesListQueryHandler(_mockWorkoutTypeRepository.Object, _mapper);
            
            var deleteHandler = new DeleteWorkoutTypeCommandHandler(_mockWorkoutTypeRepository.Object);

            var workoutTypesCount = _workoutTypeDummyList.Count;

            for (int i = 1; i <= workoutTypesCount; i++)
            {
                await deleteHandler.Handle(new DeleteWorkoutTypeCommand { WorkoutTypeId = i}, CancellationToken.None);
            }

            // act
            Func<Task> result = async () => await getHandler.Handle(new GetWorkoutTypesListQuery(), CancellationToken.None);

            // assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}
