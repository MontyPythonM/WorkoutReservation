using AutoMapper;
using FluentAssertions;
using Moq;
using WorkoutReservation.Application.Common.Exceptions;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.UpdateWorkoutType;
using WorkoutReservation.Application.MappingProfile;
using WorkoutReservation.Application.UnitTests.Mocks;
using WorkoutReservation.Domain.Entities;
using WorkoutReservation.Domain.Enums;

namespace WorkoutReservation.Application.UnitTests.WorkoutTypes
{
    public class UpdateWorkoutTypeCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IWorkoutTypeRepository> _mockWorkoutTypeRepository;
        private readonly List<WorkoutType> _workoutTypeDummyList;

        public UpdateWorkoutTypeCommandHandlerTest()
        {
            _mockWorkoutTypeRepository = WorkoutTypeRepositoryMock.GetRepositoryMock();
            _workoutTypeDummyList = WorkoutTypeRepositoryMock.GetDummyList();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WorkoutTypeProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        private const string IncorrectNameProporty = "MoreThan50Chars - Lorem ipsum dolor sit amet, consectetur";
        private const string IncorrectDescriptionProporty = "MoreThan600Chars - On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided.";

        [Theory]
        [InlineData(IncorrectNameProporty, "correctDescription", 0)]
        [InlineData("correctName", IncorrectDescriptionProporty, 1)]
        [InlineData(IncorrectNameProporty, IncorrectDescriptionProporty, 2)]
        [InlineData("correctName", "", 3)]
        [InlineData("", "correctDescription", 3)]
        [InlineData("", "", 3)]
        [InlineData("correctName", "correctDescription", 5)]
        [InlineData("correctName", "correctDescription", 0)]
        [InlineData("correctName", "correctDescription", -5)]
        public async Task Handle_ForInvalidRequest_ThrowValidationException(string name, string description, int workoutIntensity)
        {
            // arrange
            var handler = new UpdateWorkoutTypeCommandHandler(_mockWorkoutTypeRepository.Object, _mapper);

            var dummyWorkoutType = _workoutTypeDummyList.First();

            var command = new UpdateWorkoutTypeCommand()
            {
                WorkoutTypeId = dummyWorkoutType.Id,
                Name = name,
                Description = description,
                Intensity = (WorkoutIntensity)workoutIntensity
            };

            // act
            Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);

            // assert
            await result.Should().ThrowAsync<BadRequestException>();
        }

        [Fact]
        public async Task Handle_ForNotExistWorkoutType_ThrowNotFoundException()
        {
            // arrange
            var handler = new UpdateWorkoutTypeCommandHandler(_mockWorkoutTypeRepository.Object, _mapper);

            var dummyWorkoutType = _workoutTypeDummyList.Last();
            var notExistWorkoutType = dummyWorkoutType.Id + 1;

            var command = new UpdateWorkoutTypeCommand()
            {
                WorkoutTypeId = notExistWorkoutType,
                Name = "correctName",
                Description = "correctDescription",
                Intensity = WorkoutIntensity.vigorous
            };

            // act
            Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);

            // assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}
