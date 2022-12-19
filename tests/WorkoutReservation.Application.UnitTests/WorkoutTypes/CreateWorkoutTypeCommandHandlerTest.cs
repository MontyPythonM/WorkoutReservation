﻿using AutoMapper;
using FluentAssertions;
using Moq;
using WorkoutReservation.Application.Common.MappingProfile;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.WorkoutTypes.Commands.CreateWorkoutType;
using WorkoutReservation.Application.UnitTests.Mocks;
using WorkoutReservation.Domain.Enums;
using Xunit;

namespace WorkoutReservation.Application.UnitTests.WorkoutTypes;

public class CreateWorkoutTypeCommandHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IWorkoutTypeRepository> _mockWorkoutTypeRepository;
    private readonly Mock<IWorkoutTypeTagRepository> _mockWorkoutTypeTagRepository;
    private readonly Mock<IInstructorRepository> _mockInstructorRepository;

    public CreateWorkoutTypeCommandHandlerTest(Mock<IWorkoutTypeTagRepository> mockWorkoutTypeTagRepository, Mock<IInstructorRepository> mockInstructorRepository)
    {
        _mockWorkoutTypeTagRepository = mockWorkoutTypeTagRepository;
        _mockInstructorRepository = mockInstructorRepository;
        _mockWorkoutTypeRepository = WorkoutTypeRepositoryMock.GetRepositoryMock();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WorkoutTypeProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    private const string IncorrectName = "MoreThan50Chars - Lorem ipsum dolor sit amet, consectetur";
    private const string IncorrectDescription = "MoreThan600Chars - On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided.";

    [Theory]
    [InlineData(IncorrectName, "correctDescription", 0)]
    [InlineData("correctName", IncorrectDescription, 1)]
    [InlineData(IncorrectName, IncorrectDescription, 2)]
    [InlineData("correctName", "", 3)]
    [InlineData("", "correctDescription", 3)]
    [InlineData("", "", 3)]
    [InlineData("correctName", "correctDescription", 5)]
    [InlineData("correctName", "correctDescription", 0)]
    [InlineData("correctName", "correctDescription", -5)]
    public async Task Handle_NotValidRequest_ThrowValidationException(string name, string description, int workoutIntensity)
    {
        // arrange
        var handler = new CreateWorkoutTypeCommandHandler(_mapper, _mockWorkoutTypeRepository.Object, _mockWorkoutTypeTagRepository.Object, _mockInstructorRepository.Object);
        var command = new CreateWorkoutTypeCommand()
        {
            Name = name,
            Description = description,
            Intensity = (WorkoutIntensity)workoutIntensity
        };

        // act
        Func<Task> result = async () => await handler.Handle(command, CancellationToken.None);

        // assert
        await result.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Fact]
    public async Task Handle_ValidRequest_AddWorkoutTypeToRepository()
    {
        // arrange
        var handler = new CreateWorkoutTypeCommandHandler(_mapper, _mockWorkoutTypeRepository.Object, _mockWorkoutTypeTagRepository.Object, _mockInstructorRepository.Object);

        var command = new CreateWorkoutTypeCommand()
        {
            Name = "correctName",
            Description = "correctDescriptiopn",
            Intensity = WorkoutIntensity.Low
        };

        var allWorkoutTypesBeforeCount = WorkoutTypeRepositoryMock.GetDummyList().Count;

        // act
        var result = await handler.Handle(command, CancellationToken.None);
        var allWorkoutTypesAfterCount = (await _mockWorkoutTypeRepository.Object.GetAllAsync(false, CancellationToken.None)).Count;

        // assert
        result.Should().NotBe(null);
        allWorkoutTypesAfterCount.Should().Be(allWorkoutTypesBeforeCount + 1);
    }

}
