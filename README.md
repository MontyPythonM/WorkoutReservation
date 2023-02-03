# <p align="middle">WorkoutReservationWebApp<p>

### <p align="middle"> This project is a web application simulating a workout booking service made using ASP.NET Core and Angular, created for learning purposes.<p>
<br><br>
## How does it work?

The application is designed to manage the workout bookings of fitness club members. App allows Managers and Administrators to create a weekly training template (RepetitiveWorkouts), which at the start of a new week automatically creates a plan for the upcoming week (RealWorkouts). Moreover, it is possible to create and ocassional real workout. 
Visitors without accounts can view the training plan and read about workout types and instructors. Registered users can book their participation in selected workouts.
  
Application user roles:
  - Member
  - Manager
  - Bussiness Administrator
  - System Administrator
  
  The exact list of role permissions is included in the file `RolePermissionMatrix.cs` and can be modified using migration.
<br><br><br>

## Used Design Patterns
  
- #### Clean architecture
- #### CQRS
- #### Mediator
- #### Repository  
<br><br>

## Used Technologies
  
- #### .NET 6
- #### ASP.NET Core Web API
- #### Entity Framework Core 6
- #### MS SQL Database
- #### Angular 13
- #### JWT Bearer authentication

<br><br>
## Used Libraries
- #### MediatR
- #### Hangfire
- #### AutoMapper
- #### FluentValidation 
- #### xUnit, 
- #### Moq,
- #### FluentAssertions,
- #### Swagger
- #### NLog
- #### DevExtreme

