using AutoMapper;
using FluentAssertions;
using Moq;
using WorkoutReservation.Application.Contracts;
using WorkoutReservation.Application.Features.WorkoutTypes.Queries.GetWorkoutTypesList;
using WorkoutReservation.Application.MappingProfile;
using WorkoutReservation.Application.UnitTests.Mocks;
using Xunit;

namespace WorkoutReservation.Application.UnitTests.WorkoutTypes;

public class GetWorkoutTypesListQueryHandlerTest
{
    private readonly Mock<IWorkoutTypeRepository> _mockWorkoutTypeRepository;
    private readonly IMapper _mapper;

    public GetWorkoutTypesListQueryHandlerTest()
    {
        _mockWorkoutTypeRepository = WorkoutTypeRepositoryMock.GetRepositoryMock();

        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WorkoutTypeProfile>();
        });

        _mapper = configurationProvider.CreateMapper();
    }

    [Theory]       
    [InlineData(0, 5, null, null, true)]    // invalid pageNumber
    [InlineData(-1, 10, null, null, true)]
    [InlineData(-1, 15, null, null, true)]
    [InlineData(1, 1, null, null, false)]    // invalid pageSize
    [InlineData(1, 0, null, "Intensity", true)]
    [InlineData(1, -1, null, "Name", true)]
    [InlineData(1, 5, null, "name", true)]  // invalid sortBy
    [InlineData(1, 5, null, "intensity", false)]
    [InlineData(1, 5, null, "asd", false)]
    public async Task Handle_InvalidRequest_ThrowValidationException(int pageNumber, 
                                                                     int pageSize, 
                                                                     string searchPhrase, 
                                                                     string sortBy, 
                                                                     bool sortByDescending)
    {
        // arrange
        var handler = new GetWorkoutTypesListQueryHandler(_mockWorkoutTypeRepository.Object, _mapper);

        var incorrectQuery = new GetWorkoutTypesListQuery
        { 
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchPhrase = searchPhrase,
            SortBy = sortBy,
            SortByDescending = sortByDescending
        };

        // act
        Func<Task> result = async () => await handler.Handle(incorrectQuery, CancellationToken.None);

        // assert
        await result.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    [Theory]
    [InlineData(1, 5, null, null, true)]    // valid pageNumber and pageSize
    [InlineData(2, 10, null, null, false)]
    [InlineData(100, 15, null, null, true)]
    [InlineData(1, 5, null, "Name", true)]  // valid sortBy
    [InlineData(1, 5, null, "Intensity", false)] 
    public async Task Handle_ValidRequest_NotThrowValidationException(int pageNumber,
                                                                      int pageSize,
                                                                      string searchPhrase,
                                                                      string sortBy,
                                                                      bool sortByDescending)
    {
        // arrange
        var handler = new GetWorkoutTypesListQueryHandler(_mockWorkoutTypeRepository.Object, _mapper);

        var correctQuery = new GetWorkoutTypesListQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchPhrase = searchPhrase,
            SortBy = sortBy,
            SortByDescending = sortByDescending
        };

        // act
        Func<Task> result = async () => await handler.Handle(correctQuery, CancellationToken.None);

        // assert
        await result.Should().NotThrowAsync<FluentValidation.ValidationException>();
    }
}
