﻿using AutoMapper;
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
    public class DeleteWorkoutTypeCommandHandlerTest
    {
        private readonly IMapper _mapper;
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
        }

        [Fact]
        public async Task Handle_ForValidWorkoutTypeId_RemoveWorkoutType()
        {
            // arrange
            var deleteHandler = new DeleteWorkoutTypeCommandHandler(_mockWorkoutTypeRepository.Object);
            var getHandler = new GetWorkoutTypesListQueryHandler(_mockWorkoutTypeRepository.Object, _mapper);

            var workoutTypesBeforeCount = _workoutTypeDummyList.Count;

            // act
            await deleteHandler.Handle(new DeleteWorkoutTypeCommand { WorkoutTypeId = workoutTypesBeforeCount }, CancellationToken.None);

            var workoutTypesAfterCount = (await getHandler.Handle(new GetWorkoutTypesListQuery(), CancellationToken.None)).Count;

            // assert
            workoutTypesAfterCount.Should().Be(workoutTypesBeforeCount - 1);
        }

        [Fact]
        public async Task Handle_ForInvalidWorkoutTypeId_ThrowNotFoundException()
        {
            // arrange
            var deleteHandler = new DeleteWorkoutTypeCommandHandler(_mockWorkoutTypeRepository.Object);

            var notExistWorkoutType = _workoutTypeDummyList.Count + 1;

            // act
            Func<Task> result = async () => await deleteHandler.Handle(new DeleteWorkoutTypeCommand { WorkoutTypeId = notExistWorkoutType }, CancellationToken.None);

            // assert
            await result.Should().ThrowAsync<NotFoundException>();
        }
    }
}
