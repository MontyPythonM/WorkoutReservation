# <p align="middle">WorkoutReservationWebApp<p>

### <p align="middle"> This project is a Rest Api simulating a workout booking service made using ASP.NET Core, created for learning purposes. <p>

  
## How does it work?

The application is designed to manage the workout bookings of fitness club members. App allows Managers to create a weekly training template (RepetitiveWorkout), which at the start of a new week automatically creates a plan for the upcoming week (RealWorkout). Moreover, it is possible to create and manage one-time workouts. 
Visitors without accounts can view the training plan and read about workout types and instructors. Registered users can book their participation in selected workouts. When the program starts for the first time an account with Administrator role is created. 
  
In addition, a few application user roles are implemented:
  - NotComfirmedMember - new created account require email confirmation (in progress)
  - Member - can book trainings
  - Manager - require Administrator promotion. Can create and edit: workout schedule, info about instructors and workout types
  - Administrator - can almost evertything ;)
<br><br/>
## Used Design Patterns
  
- #### Clean architecture
- #### CQRS
- #### Mediator
- #### Repository  
<br><br/>
## Used Technologies
  
- #### .NET 6
- #### ASP.NET Core Web API
- #### Entity Framework Core
- #### MS SQL Database
- #### JWT Bearer authentication
  <br><br/>
## Used Libraries
- #### MediatR
- #### AutoMapper
- #### FluentValidation 
- #### xUnit (with Moq and FluentAssertions)
- #### Swagger
- #### NLog
